using GotaSoundIO;
using GotaSoundIO.IO;
using GotaSoundIO.Sound;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NitroFileLoader {

    /// <summary>
    /// Sound archive.
    /// </summary>
    public class SoundArchive : IOFile {

        /// <summary>
        /// Max sequence Id.
        /// </summary>
        public const uint MaxSequenceId = 0xFFFFFFFF;

        /// <summary>
        /// Max sequence archive Id.
        /// </summary>
        public const uint MaxSequenceArchiveId = 0xFFFFFFFF;

        /// <summary>
        /// Max bank Id.
        /// </summary>
        public const uint MaxBankId = 0xFFFF;

        /// <summary>
        /// Max wave archive Id.
        /// </summary>
        public const uint MaxWaveArchiveId = 0xFFFE;

        /// <summary>
        /// Max player Id.
        /// </summary>
        public const uint MaxPlayerId = 31;
        
        /// <summary>
        /// Max group Id.
        /// </summary>
        public const uint MaxGroupId = 0xFFFFFFFF;

        /// <summary>
        /// Max stream player Id.
        /// </summary>
        public const uint MaxStreamPlayerId = 3;

        /// <summary>
        /// Max stream Id.
        /// </summary>
        public const uint MaxStreamId = 0xFFFFFFFF;

        /// <summary>
        /// Sequences.
        /// </summary>
        public List<SequenceInfo> Sequences = new List<SequenceInfo>();

        /// <summary>
        /// Sequence archives.
        /// </summary>
        public List<SequenceArchiveInfo> SequenceArchives = new List<SequenceArchiveInfo>();

        /// <summary>
        /// Banks.
        /// </summary>
        public List<BankInfo> Banks = new List<BankInfo>();

        /// <summary>
        /// Wave archives.
        /// </summary>
        public List<WaveArchiveInfo> WaveArchives = new List<WaveArchiveInfo>();

        /// <summary>
        /// Players.
        /// </summary>
        public List<PlayerInfo> Players = new List<PlayerInfo>();

        /// <summary>
        /// Groups.
        /// </summary>
        public List<GroupInfo> Groups = new List<GroupInfo>();

        /// <summary>
        /// Stream players.
        /// </summary>
        public List<StreamPlayerInfo> StreamPlayers = new List<StreamPlayerInfo>();

        /// <summary>
        /// Streams.
        /// </summary>
        public List<StreamInfo> Streams = new List<StreamInfo>();

        /// <summary>
        /// Save symbol block.
        /// </summary>
        public bool SaveSymbols = true;

        /// <summary>
        /// Blank constructor.
        /// </summary>
        public SoundArchive() {}

        /// <summary>
        /// Create a sound archive from a file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public SoundArchive(string filePath) : base(filePath) {}

        /// <summary>
        /// Read the file.
        /// </summary>
        /// <param name="r">The reader.</param>
        public override void Read(FileReader r) {

            //Open the file.
            FileHeader header;
            r.OpenFile<SDATHeader>(out header);

            //Names.
            List<string> seqNames = new List<string>();
            List<string> seqArcNames = new List<string>();
            List<List<string>> seqArcSequenceNames = new List<List<string>>();
            List<string> bankNames = new List<string>();
            List<string> warNames = new List<string>();
            List<string> playerNames = new List<string>();
            List<string> groupNames = new List<string>();
            List<string> streamPlayerNames = new List<string>();
            List<string> streamNames = new List<string>();

            //Symbol block.
            if (header.BlockOffsets.Length > 3) {

                //Save symbols.
                SaveSymbols = true;

                //Block header.
                r.OpenBlock(0, out _, out _, false);
                r.ReadUInt64();

                //Get offsets.
                r.OpenOffset("seqNames");
                r.OpenOffset("seqArcNames");
                r.OpenOffset("bankNames");
                r.OpenOffset("warNames");
                r.OpenOffset("playerNames");
                r.OpenOffset("groupNames");
                r.OpenOffset("streamPlayerNames");
                r.OpenOffset("streamNames");

                //Read name data.
                List<string> ReadNameData(string name) {
                    List<string> s = new List<string>();
                    r.JumpToOffset(name);
                    var nameOffs = r.Read<Table<uint>>();
                    foreach (var u in nameOffs) {
                        if (u == 0) {
                            s.Add(null);
                        } else {
                            r.Jump(u);
                            s.Add(r.ReadNullTerminated());
                        }
                    }
                    return s;
                }

                //Read stuff.
                seqNames = ReadNameData("seqNames");
                bankNames = ReadNameData("bankNames");
                warNames = ReadNameData("warNames");
                playerNames = ReadNameData("playerNames");
                groupNames = ReadNameData("groupNames");
                streamPlayerNames = ReadNameData("streamPlayerNames");
                streamNames = ReadNameData("streamNames");

                //Sequence archives.
                r.JumpToOffset("seqArcNames");
                uint numSeqArcs = r.ReadUInt32();
                for (uint i = 0; i < numSeqArcs; i++) {
                    r.OpenOffset("seqArcName" + i);
                    r.OpenOffset("seqArcSequenceNames" + i);
                }

                //Read sequence archive names.
                for (uint i = 0; i < numSeqArcs; i++) {
                    if (!r.OffsetNull("seqArcName" + i)) {
                        r.JumpToOffset("seqArcName" + i);
                        seqArcNames.Add(r.ReadNullTerminated());
                    } else {
                        seqArcNames.Add(null);
                    }
                    if (!r.OffsetNull("seqArcSequenceNames" + i)) {
                        seqArcSequenceNames.Add(ReadNameData("seqArcSequenceNames" + i));
                    } else {
                        seqArcSequenceNames.Add(null);
                    }
                }

            } else {
                SaveSymbols = false;
            }

            //FAT block.
            r.OpenBlock(header.BlockOffsets.Length > 3 ? 2 : 1, out _, out _);

            //Read entries.
            uint numFiles = r.ReadUInt32();
            List<Tuple<uint, uint>> fileOffs = new List<Tuple<uint, uint>>();
            for (uint i = 0; i < numFiles; i++) {
                fileOffs.Add(new Tuple<uint, uint>(r.ReadUInt32(), r.ReadUInt32()));
                r.ReadUInt64();
            }

            //Info block.
            r.OpenBlock(header.BlockOffsets.Length > 3 ? 1 : 0, out _, out _, false);
            r.ReadUInt64();

            //Open offsets.
            Sequences = new List<SequenceInfo>();
            SequenceArchives = new List<SequenceArchiveInfo>();
            Banks = new List<BankInfo>();
            WaveArchives = new List<WaveArchiveInfo>();
            Players = new List<PlayerInfo>();
            Groups = new List<GroupInfo>();
            StreamPlayers = new List<StreamPlayerInfo>();
            Streams = new List<StreamInfo>();
            r.OpenOffset("seqInfo");
            r.OpenOffset("seqArcInfo");
            r.OpenOffset("bankInfo");
            r.OpenOffset("warInfo");
            r.OpenOffset("playerInfo");
            r.OpenOffset("groupInfo");
            r.OpenOffset("streamPlayerInfo");
            r.OpenOffset("streamInfo");

            //Keep track of MD5Sums and unique IDs.
            Dictionary<string, List<uint>> md5Ids = new Dictionary<string, List<uint>>();

            //Read player info.
            r.JumpToOffset("playerInfo");
            var offs = r.Read<Table<uint>>();
            int ind = 0;
            foreach (var o in offs) {
                if (o != 0) {
                    r.Jump(o);
                    Players.Add(r.Read<PlayerInfo>());
                    Players.Last().Index = ind;
                    Players.Last().Name = ind > (playerNames.Count - 1) ? "PLAYER_" + ind : playerNames[ind];
                }
                ind++;
            }

            //Read stream player info.
            r.JumpToOffset("streamPlayerInfo");
            offs = r.Read<Table<uint>>();
            ind = 0;
            foreach (var o in offs) {
                if (o != 0) {
                    r.Jump(o);
                    StreamPlayers.Add(r.Read<StreamPlayerInfo>());
                    StreamPlayers.Last().Index = ind;
                    StreamPlayers.Last().Name = ind > (streamPlayerNames.Count - 1) ? "STRM_PLAYER_" + ind : streamPlayerNames[ind];
                }
                ind++;
            }

            //Read wave archive info.
            r.JumpToOffset("warInfo");
            offs = r.Read<Table<uint>>();
            ind = 0;
            foreach (var o in offs) {
                if (o != 0) {
                    r.Jump(o);
                    WaveArchives.Add(r.Read<WaveArchiveInfo>());
                    WaveArchives.Last().Index = ind;
                    WaveArchives.Last().Name = ind > (warNames.Count - 1) ? "WAVE_ARCHIVE_" + ind : warNames[ind];
                    r.Jump(fileOffs[(int)WaveArchives.Last().ReadingFileId].Item1, true);
                    WaveArchives.Last().File = r.ReadFile<WaveArchive>() as WaveArchive;
                    string md5 = WaveArchives.Last().File.Md5Sum;
                    if (!md5Ids.ContainsKey(md5)) {
                        md5Ids.Add(md5, new List<uint>() { WaveArchives.Last().ReadingFileId });
                    } else {
                        if (!md5Ids[md5].Contains(WaveArchives.Last().ReadingFileId)) {
                            WaveArchives.Last().ForceIndividualFile = true;
                        }
                    }
                }
                ind++;
            }

            //Read bank info.
            r.JumpToOffset("bankInfo");
            offs = r.Read<Table<uint>>();
            ind = 0;
            foreach (var o in offs) {
                if (o != 0) {
                    r.Jump(o);
                    Banks.Add(r.Read<BankInfo>());
                    Banks.Last().Index = ind;
                    Banks.Last().Name = ind > (bankNames.Count - 1) ? "BANK_" + ind : bankNames[ind];
                    r.Jump(fileOffs[(int)Banks.Last().ReadingFileId].Item1, true);
                    Banks.Last().File = r.ReadFile<Bank>() as Bank;
                    Banks.Last().WaveArchives[0] = Banks.Last().ReadingWave0Id == 0xFFFF ? null : WaveArchives.Where(x => x.Index == Banks.Last().ReadingWave0Id).FirstOrDefault();
                    Banks.Last().WaveArchives[1] = Banks.Last().ReadingWave1Id == 0xFFFF ? null : WaveArchives.Where(x => x.Index == Banks.Last().ReadingWave1Id).FirstOrDefault();
                    Banks.Last().WaveArchives[2] = Banks.Last().ReadingWave2Id == 0xFFFF ? null : WaveArchives.Where(x => x.Index == Banks.Last().ReadingWave2Id).FirstOrDefault();
                    Banks.Last().WaveArchives[3] = Banks.Last().ReadingWave3Id == 0xFFFF ? null : WaveArchives.Where(x => x.Index == Banks.Last().ReadingWave3Id).FirstOrDefault();
                    string md5 = Banks.Last().File.Md5Sum;
                    if (!md5Ids.ContainsKey(md5)) {
                        md5Ids.Add(md5, new List<uint>() { Banks.Last().ReadingFileId });
                    } else {
                        if (!md5Ids[md5].Contains(Banks.Last().ReadingFileId)) {
                            Banks.Last().ForceIndividualFile = true;
                        }
                    }
                }
                ind++;
            }

            //Read sequence info.
            r.JumpToOffset("seqInfo");
            offs = r.Read<Table<uint>>();
            ind = 0;
            foreach (var o in offs) {
                if (o != 0) {
                    r.Jump(o);
                    Sequences.Add(r.Read<SequenceInfo>());
                    Sequences.Last().Index = ind;
                    Sequences.Last().Name = ind > (seqNames.Count - 1) ? "SEQ_" + ind : seqNames[ind];
                    r.Jump(fileOffs[(int)Sequences.Last().ReadingFileId].Item1, true);
                    Sequences.Last().File = r.ReadFile<Sequence>() as Sequence;
                    Sequences.Last().Bank = Banks.Where(x => x.Index == Sequences.Last().ReadingBankId).FirstOrDefault();
                    Sequences.Last().Player = Players.Where(x => x.Index == Sequences.Last().ReadingPlayerId).FirstOrDefault();
                    string md5 = Sequences.Last().File.Md5Sum;
                    if (!md5Ids.ContainsKey(md5)) {
                        md5Ids.Add(md5, new List<uint>() { Sequences.Last().ReadingFileId });
                    } else {
                        if (!md5Ids[md5].Contains(Sequences.Last().ReadingFileId)) {
                            Sequences.Last().ForceIndividualFile = true;
                        }
                    }
                }
                ind++;
            }

            //Read stream info.
            r.JumpToOffset("streamInfo");
            offs = r.Read<Table<uint>>();
            ind = 0;
            foreach (var o in offs) {
                if (o != 0) {
                    r.Jump(o);
                    Streams.Add(r.Read<StreamInfo>());
                    Streams.Last().Index = ind;
                    Streams.Last().Name = ind > (streamNames.Count - 1) ? "STRM_" + ind : streamNames[ind];
                    r.Jump(fileOffs[(int)Streams.Last().ReadingFileId].Item1, true);
                    Streams.Last().File = r.ReadFile<Stream>() as Stream;
                    Streams.Last().Player = StreamPlayers.Where(x => x.Index == Streams.Last().ReadingPlayerId).FirstOrDefault();
                    string md5 = Streams.Last().File.Md5Sum;
                    if (!md5Ids.ContainsKey(md5)) {
                        md5Ids.Add(md5, new List<uint>() { Streams.Last().ReadingFileId });
                    } else {
                        if (!md5Ids[md5].Contains(Streams.Last().ReadingFileId)) {
                            Streams.Last().ForceIndividualFile = true;
                        }
                    }
                }
                ind++;
            }

            //Read sequence archive info.
            r.JumpToOffset("seqArcInfo");
            offs = r.Read<Table<uint>>();
            ind = 0;
            foreach (var o in offs) {
                if (o != 0) {
                    r.Jump(o);
                    SequenceArchives.Add(r.Read<SequenceArchiveInfo>());
                    SequenceArchives.Last().Index = ind;
                    SequenceArchives.Last().Name = ind > (seqArcNames.Count - 1) ? "SEQARC_" + ind : seqArcNames[ind];
                    r.Jump(fileOffs[(int)SequenceArchives.Last().ReadingFileId].Item1, true);
                    SequenceArchives.Last().File = r.ReadFile<SequenceArchive>() as SequenceArchive;
                    var labels = SequenceArchives.Last().File.Labels;
                    SequenceArchives.Last().File.Labels = new Dictionary<string, uint>();
                    if (SequenceArchives.Last().File.Sequences.Count > 0) {
                        int seqNum = 0;
                        for (int i = 0; i <= SequenceArchives.Last().File.Sequences.Last().Index; i++) {
                            string defName = "Sequence_" + i;
                            try { defName = seqArcSequenceNames[ind][i]; } catch { }
                            var e = SequenceArchives.Last().File.Sequences.Where(x => x.Index == i).FirstOrDefault();
                            if (defName != null && e != null) {
                                e.Name = defName;
                                e.Bank = Banks.Where(x => x.Index == e.ReadingBankId).FirstOrDefault();
                                e.Player = Players.Where(x => x.Index == e.ReadingPlayerId).FirstOrDefault();
                                if (!SequenceArchives.Last().File.Labels.ContainsKey(defName)) {
                                    SequenceArchives.Last().File.Labels.Add(defName, labels.Values.ElementAt(seqNum));
                                }
                                seqNum++;
                            }
                        }
                    }
                    string md5 = SequenceArchives.Last().File.Md5Sum;
                    if (!md5Ids.ContainsKey(md5)) {
                        md5Ids.Add(md5, new List<uint>() { SequenceArchives.Last().ReadingFileId });
                    } else {
                        if (!md5Ids[md5].Contains(SequenceArchives.Last().ReadingFileId)) {
                            SequenceArchives.Last().ForceIndividualFile = true;
                        }
                    }
                }
                ind++;
            }

            //Read group info.
            r.JumpToOffset("groupInfo");
            offs = r.Read<Table<uint>>();
            ind = 0;
            foreach (var o in offs) {
                if (o != 0) {
                    r.Jump(o);
                    Groups.Add(r.Read<GroupInfo>());
                    Groups.Last().Index = ind;
                    Groups.Last().Name = ind > (groupNames.Count - 1) ? "GROUP_" + ind : groupNames[ind];
                    for (int i = 0; i < Groups.Last().Entries.Count; i++) {
                        switch (Groups.Last().Entries[i].Type) {
                            case GroupEntryType.Sequence:
                                Groups.Last().Entries[i].Entry = Sequences.Where(x => x.Index == (int)Groups.Last().Entries[i].ReadingId).FirstOrDefault();
                                break;
                            case GroupEntryType.Bank:
                                Groups.Last().Entries[i].Entry = Banks.Where(x => x.Index == (int)Groups.Last().Entries[i].ReadingId).FirstOrDefault();
                                    break;
                            case GroupEntryType.WaveArchive:
                                Groups.Last().Entries[i].Entry = WaveArchives.Where(x => x.Index == (int)Groups.Last().Entries[i].ReadingId).FirstOrDefault();
                                    break;
                            case GroupEntryType.SequenceArchive:
                                Groups.Last().Entries[i].Entry = SequenceArchives.Where(x => x.Index == (int)Groups.Last().Entries[i].ReadingId).FirstOrDefault();
                                    break;
                        }
                    }
                }
                ind++;
            }

        }

        /// <summary>
        /// Write the file.
        /// </summary>
        /// <param name="w">The writer.</param>
        public override void Write(FileWriter w) {

            //Init file.
            w.InitFile<SDATHeader>("SDAT", ByteOrder.LittleEndian, null, SaveSymbols ? 4 : 3);

            //Sort everything.
            Sequences = Sequences.OrderBy(x => x.Index).ToList();
            SequenceArchives = SequenceArchives.OrderBy(x => x.Index).ToList();
            Banks = Banks.OrderBy(x => x.Index).ToList();
            WaveArchives = WaveArchives.OrderBy(x => x.Index).ToList();
            Players = Players.OrderBy(x => x.Index).ToList();
            Groups = Groups.OrderBy(x => x.Index).ToList();
            StreamPlayers = StreamPlayers.OrderBy(x => x.Index).ToList();
            Streams = Streams.OrderBy(x => x.Index).ToList();

            //Symbol block.
            if (SaveSymbols) {

                //Init block.
                w.InitBlock("SYMB", false, true);
                w.Write("SYMB".ToCharArray());
                w.Write((uint)0);
                w.InitOffset("seqStrings");
                w.InitOffset("seqArcStrings");
                w.InitOffset("bnkStrings");
                w.InitOffset("warStrings");
                w.InitOffset("plyStrings");
                w.InitOffset("grpStrings");
                w.InitOffset("stmPlyStrings");
                w.InitOffset("stmStrings");
                w.Write(new uint[6]);

                //Prepare the string table.
                long prepareStringTable(uint maxEntries) {
                    long h = w.Position;
                    w.Write(maxEntries);
                    w.Write(new uint[maxEntries]);
                    return h;
                }

                //Prepare tables.
                long seqSBak = 0;
                w.CloseOffset("seqStrings");
                try { seqSBak = prepareStringTable((uint)(Sequences.Last().Index + 1)); } catch { w.Write((uint)0); }

                //Preparing the sequence archives is a bit more tricky.
                w.CloseOffset("seqArcStrings");
                if (SequenceArchives.Count > 1) {
                    w.Write((uint)(SequenceArchives.Last().Index + 1));
                    for (int i = 0; i <= SequenceArchives.Last().Index; i++) {
                        if (SequenceArchives.Where(x => x.Index == i).Count() < 1) { continue; }
                        w.InitOffset("seqArcS" + i);
                        w.InitOffset("seqArcSubS" + i);
                    }
                } else { w.Write((uint)0); }

                //Sub entries.
                List<long> seqArcSeqSBak = new List<long>();
                if (SequenceArchives.Count > 1) {
                    for (int i = 0; i <= SequenceArchives.Last().Index; i++) {
                        if (SequenceArchives.Where(x => x.Index == i).Count() < 1) { seqArcSeqSBak.Add(0); continue; }
                        var e = SequenceArchives.Where(x => x.Index == i).FirstOrDefault();
                        w.CloseOffset("seqArcSubS" + i);
                        seqArcSeqSBak.Add(w.Position);
                        if (e.File.Sequences.Count > 0) {
                            w.Write((uint)(e.File.Sequences.Last().Index + 1));
                            w.Write(new uint[e.File.Sequences.Last().Index + 1]);
                        } else {
                            w.Write((uint)0);
                        }
                    }
                }

                //Prepare more tables.
                long bankSBak = 0;
                w.CloseOffset("bnkStrings");
                try { bankSBak = prepareStringTable((uint)(Banks.Last().Index + 1)); } catch { w.Write((uint)0); }
                long warSBak = 0;
                w.CloseOffset("warStrings");
                try { warSBak = prepareStringTable((uint)(WaveArchives.Last().Index + 1)); } catch { w.Write((uint)0); }
                long plySBak = 0;
                w.CloseOffset("plyStrings");
                try { plySBak = prepareStringTable((uint)(Players.Last().Index + 1)); } catch { w.Write((uint)0); }
                long grpSBak = 0;
                w.CloseOffset("grpStrings");
                try { grpSBak = prepareStringTable((uint)(Groups.Last().Index + 1)); } catch { w.Write((uint)0); }
                long stmPlySBak = 0;
                w.CloseOffset("stmPlyStrings");
                try { stmPlySBak = prepareStringTable((uint)(StreamPlayers.Last().Index + 1)); } catch { w.Write((uint)0); }
                long stmSBak = 0;
                w.CloseOffset("stmStrings");
                try { stmSBak = prepareStringTable((uint)(Streams.Last().Index + 1)); } catch { w.Write((uint)0); }

                //Write a string.
                void writeStringData(object entryList, long tablePos) {
                    List<string> strgs = new List<string>();
                    var seqList = entryList as List<SequenceInfo>;
                    var bnkList = entryList as List<BankInfo>;
                    var warList = entryList as List<WaveArchiveInfo>;
                    var plyList = entryList as List<PlayerInfo>;
                    var grpList = entryList as List<GroupInfo>;
                    var stmPlyList = entryList as List<StreamPlayerInfo>;
                    var stmList = entryList as List<StreamInfo>;
                    if (seqList != null) {
                        for (int i = 0; i <= seqList.Last().Index; i++) {
                            var y = seqList.Where(x => x.Index == i);
                            if (y.Count() > 0) {
                                strgs.Add(y.FirstOrDefault().Name);
                            } else {
                                strgs.Add(null);
                            }
                        }
                    }
                    if (bnkList != null) {
                        for (int i = 0; i <= bnkList.Last().Index; i++) {
                            var y = bnkList.Where(x => x.Index == i);
                            if (y.Count() > 0) {
                                strgs.Add(y.FirstOrDefault().Name);
                            } else {
                                strgs.Add(null);
                            }
                        }
                    }
                    if (warList != null) {
                        for (int i = 0; i <= warList.Last().Index; i++) {
                            var y = warList.Where(x => x.Index == i);
                            if (y.Count() > 0) {
                                strgs.Add(y.FirstOrDefault().Name);
                            } else {
                                strgs.Add(null);
                            }
                        }
                    }
                    if (plyList != null) {
                        for (int i = 0; i <= plyList.Last().Index; i++) {
                            var y = plyList.Where(x => x.Index == i);
                            if (y.Count() > 0) {
                                strgs.Add(y.FirstOrDefault().Name);
                            } else {
                                strgs.Add(null);
                            }
                        }
                    }
                    if (grpList != null) {
                        for (int i = 0; i <= grpList.Last().Index; i++) {
                            var y = grpList.Where(x => x.Index == i);
                            if (y.Count() > 0) {
                                strgs.Add(y.FirstOrDefault().Name);
                            } else {
                                strgs.Add(null);
                            }
                        }
                    }
                    if (stmPlyList != null) {
                        for (int i = 0; i <= stmPlyList.Last().Index; i++) {
                            var y = stmPlyList.Where(x => x.Index == i);
                            if (y.Count() > 0) {
                                strgs.Add(y.FirstOrDefault().Name);
                            } else {
                                strgs.Add(null);
                            }
                        }
                    }
                    if (stmList != null) {
                        for (int i = 0; i <= stmList.Last().Index; i++) {
                            var y = stmList.Where(x => x.Index == i);
                            if (y.Count() > 0) {
                                strgs.Add(y.FirstOrDefault().Name);
                            } else {
                                strgs.Add(null);
                            }
                        }
                    }

                    //Write strg offset.
                    for (int i = 0; i < strgs.Count; i++) {
                        if (strgs[i] == null) { continue; }
                        long bak = w.Position;
                        w.Position = tablePos + 4 + 4 * i;
                        w.Write((uint)(bak - w.CurrentOffset));
                        w.Position = bak;
                        w.WriteNullTerminated(strgs[i]);
                    }

                }

                //Write string data.
                try { writeStringData(Sequences, seqSBak); } catch { }

                //Write sequence archives.
                if (SequenceArchives.Count > 0) {
                    for (int i = 0; i <= SequenceArchives.Last().Index; i++) {
                        var e = SequenceArchives.Where(x => x.Index == i).FirstOrDefault();
                        if (e != null) {

                            //Close offset.
                            w.CloseOffset("seqArcS" + i);
                            w.WriteNullTerminated(e.Name);

                            //Write names.
                            if (e.File.Sequences.Count > 0) {
                                for (int j = 0; j <= e.File.Sequences.Last().Index; j++) {
                                    var f = e.File.Sequences.Where(x => x.Index == j).FirstOrDefault();
                                    if (f == null) { continue; }
                                    long currPos = w.Position;
                                    w.Position = seqArcSeqSBak[i] + 4 + 4 * f.Index;
                                    w.Write((uint)(currPos - w.CurrentOffset));
                                    w.Position = currPos;
                                    w.WriteNullTerminated(f.Name);
                                }
                            }

                        }
                    }
                }

                //Write more string data.
                try { writeStringData(Banks, bankSBak); } catch { }
                try { writeStringData(WaveArchives, warSBak); } catch { }
                try { writeStringData(Players, plySBak); } catch { }
                try { writeStringData(Groups, grpSBak); } catch { }
                try { writeStringData(StreamPlayers, stmPlySBak); } catch { }
                try { writeStringData(Streams, stmSBak); } catch { }

                //Close block.
                long beforePadPosS = w.Position;
                w.Pad(4);
                long afterPadPosS = w.Position;
                w.CloseBlock();
                w.BlockSizes[w.BlockSizes.Count - 1] -= afterPadPosS - beforePadPosS;

            }

            //Get files.
            Dictionary<string, Tuple<IOFile, uint>> files = new Dictionary<string, Tuple<IOFile, uint>>();
            uint fileId = 0;
            foreach (var e in Sequences) {
                string md5 = e.File.Md5Sum;
                if (e.ForceIndividualFile) {
                    e.ReadingFileId = fileId;
                    files.Add(md5 + fileId, new Tuple<IOFile, uint>(e.File, fileId++));
                } else if (!files.ContainsKey(md5)) {
                    e.ReadingFileId = fileId;
                    files.Add(md5, new Tuple<IOFile, uint>(e.File, fileId++));
                } else {
                    e.ReadingFileId = files[md5].Item2;
                }
            }
            foreach (var e in SequenceArchives) {
                string md5 = e.File.Md5Sum;
                if (e.ForceIndividualFile) {
                    e.ReadingFileId = fileId;
                    files.Add(md5 + fileId, new Tuple<IOFile, uint>(e.File, fileId++));
                } else if (!files.ContainsKey(md5)) {
                    e.ReadingFileId = fileId;
                    files.Add(md5, new Tuple<IOFile, uint>(e.File, fileId++));
                } else {
                    e.ReadingFileId = files[md5].Item2;
                }
            }
            foreach (var e in Banks) {
                string md5 = e.File.Md5Sum;
                if (e.ForceIndividualFile) {
                    e.ReadingFileId = fileId;
                    files.Add(md5 + fileId, new Tuple<IOFile, uint>(e.File, fileId++));
                } else if (!files.ContainsKey(md5)) {
                    e.ReadingFileId = fileId;
                    files.Add(md5, new Tuple<IOFile, uint>(e.File, fileId++));
                } else {
                    e.ReadingFileId = files[md5].Item2;
                }
            }
            foreach (var e in WaveArchives) {
                string md5 = e.File.Md5Sum;
                if (e.ForceIndividualFile) {
                    e.ReadingFileId = fileId;
                    files.Add(md5 + fileId, new Tuple<IOFile, uint>(e.File, fileId++));
                } else if (!files.ContainsKey(md5)) {
                    e.ReadingFileId = fileId;
                    files.Add(md5, new Tuple<IOFile, uint>(e.File, fileId++));
                } else {
                    e.ReadingFileId = files[md5].Item2;
                }
            }
            foreach (var e in Streams) {
                string md5 = e.File.Md5Sum;
                if (e.ForceIndividualFile) {
                    e.ReadingFileId = fileId;
                    files.Add(md5 + fileId, new Tuple<IOFile, uint>(e.File, fileId++));
                } else if (!files.ContainsKey(md5)) {
                    e.ReadingFileId = fileId;
                    files.Add(md5, new Tuple<IOFile, uint>(e.File, fileId++));
                } else {
                    e.ReadingFileId = files[md5].Item2;
                }
            }

            //Write info block.
            w.InitBlock("INFO");
            w.CurrentOffset -= 8;
            long infoOff = w.Position - 8;

            //Init offsets.
            w.InitOffset("seqInfo");
            w.InitOffset("seqArcInfo");
            w.InitOffset("bnkInfo");
            w.InitOffset("warInfo");
            w.InitOffset("plyInfo");
            w.InitOffset("grpInfo");
            w.InitOffset("stmPlyInfo");
            w.InitOffset("stmInfo");
            w.Write(new uint[6]);

            //Prepare an info table.
            long prepareInfoTable(uint maxEntries) {
                long h = w.Position;
                w.Write(maxEntries);
                w.Write(new uint[maxEntries]);
                return h;
            }

            //Prepare table.
            long seqIBak = 0;
            w.CloseOffset("seqInfo");
            try { seqIBak = prepareInfoTable((uint)(Sequences.Last().Index + 1)); } catch { w.Write((uint)0); }

            //Write data.
            if (Sequences.Count() > 0) {
                foreach (var e in Sequences) {
                    long bak = w.Position;
                    w.Position = seqIBak + 4 + 4 * e.Index;
                    w.Write((uint)(bak - infoOff));
                    w.Position = bak;
                    w.Write(e);
                }
            }

            //Prepare table.
            long seqArcIBak = 0;
            w.CloseOffset("seqArcInfo");
            try { seqArcIBak = prepareInfoTable((uint)(SequenceArchives.Last().Index + 1)); } catch { w.Write((uint)0); }

            //Write info.
            if (SequenceArchives.Count() > 0) {
                foreach (var e in SequenceArchives) {
                    long bak = w.Position;
                    w.Position = seqArcIBak + 4 + 4 * e.Index;
                    w.Write((uint)(bak - infoOff));
                    w.Position = bak;
                    w.Write(e);
                }
            }

            //Prepare table.
            long bankIBak = 0;
            w.CloseOffset("bnkInfo");
            try { bankIBak = prepareInfoTable((uint)(Banks.Last().Index + 1)); } catch { w.Write((uint)0); }

            //Write info.
            if (Banks.Count() > 0) {
                foreach (var e in Banks) {
                    long bak = w.Position;
                    w.Position = bankIBak + 4 + 4 * e.Index;
                    w.Write((uint)(bak - infoOff));
                    w.Position = bak;
                    w.Write(e);
                }
            }

            //Prepare table.
            long warIBak = 0;
            w.CloseOffset("warInfo");
            try { warIBak = prepareInfoTable((uint)(WaveArchives.Last().Index + 1)); } catch { w.Write((uint)0); }

            //Write info.
            if (WaveArchives.Count() > 0) {
                foreach (var e in WaveArchives) {
                    long bak = w.Position;
                    w.Position = warIBak + 4 + 4 * e.Index;
                    w.Write((uint)(bak - infoOff));
                    w.Position = bak;
                    w.Write(e);
                }
            }

            //Prepare table.
            long plyIBak = 0;
            w.CloseOffset("plyInfo");
            try { plyIBak = prepareInfoTable((uint)(Players.Last().Index + 1)); } catch { w.Write((uint)0); }

            //Write info.
            if (Players.Count() > 0) {
                foreach (var e in Players) {
                    long bak = w.Position;
                    w.Position = plyIBak + 4 + 4 * e.Index;
                    w.Write((uint)(bak - infoOff));
                    w.Position = bak;
                    w.Write(e);
                }
            }

            //Prepare table.
            long grpIBak = 0;
            w.CloseOffset("grpInfo");
            try { grpIBak = prepareInfoTable((uint)(Groups.Last().Index + 1)); } catch { w.Write((uint)0); }

            //Write info.
            if (Groups.Count() > 0) {
                foreach (var e in Groups) {
                    long bak = w.Position;
                    w.Position = grpIBak + 4 + 4 * e.Index;
                    w.Write((uint)(bak - infoOff));
                    w.Position = bak;
                    w.Write(e);
                }
            }

            //Prepare table.
            long stmPlyIBak = 0;
            w.CloseOffset("stmPlyInfo");
            try { stmPlyIBak = prepareInfoTable((uint)(StreamPlayers.Last().Index + 1)); } catch { w.Write((uint)0); }

            //Write info.
            if (StreamPlayers.Count() > 0) {
                foreach (var e in StreamPlayers) {
                    long bak = w.Position;
                    w.Position = stmPlyIBak + 4 + 4 * e.Index;
                    w.Write((uint)(bak - infoOff));
                    w.Position = bak;
                    w.Write(e);
                }
            }

            //Prepare table.
            long stmIBak = 0;
            w.CloseOffset("stmInfo");
            try { stmIBak = prepareInfoTable((uint)(Streams.Last().Index + 1)); } catch { w.Write((uint)0); }

            //Write info.
            if (Streams.Count() > 0) {
                foreach (var e in Streams) {
                    long bak = w.Position;
                    w.Position = stmIBak + 4 + 4 * e.Index;
                    w.Write((uint)(bak - infoOff));
                    w.Position = bak;
                    w.Write(e);
                }
            }

            //Close info block.
            long beforePadPosI = w.Position;
            w.Pad(4);
            long afterPadPosI = w.Position;
            w.CloseBlock();
            w.BlockSizes[w.BlockSizes.Count - 1] -= afterPadPosI - beforePadPosI;

            //Fat block.
            w.InitBlock("FAT ");

            //Get binaries.
            List<byte[]> filesRaw = new List<byte[]>();
            foreach (var f in files) {
                filesRaw.Add(f.Value.Item1.Write());
            }

            //Init offsets.
            w.Write((uint)files.Count);
            for (int i = 0; i < filesRaw.Count; i++) {
                w.InitOffset("file" + i);
                w.Write((uint)filesRaw[i].Length);
                w.Write((ulong)0);
            }

            //Close block.
            long beforePadPosFAT = w.Position;
            w.Pad(4);
            long afterPadPosFAT = w.Position;
            w.CloseBlock();
            w.BlockSizes[w.BlockSizes.Count - 1] -= afterPadPosFAT - beforePadPosFAT;

            //File block.
            w.InitBlock("FILE");

            //Write files.
            w.Write((uint)filesRaw.Count);
            w.Pad(0x20);
            for (int i = 0; i < filesRaw.Count; i++) {
                w.CloseOffset("file" + i, true);
                w.Write(filesRaw[i]);
                w.Pad(0x20);
            }

            //Close block.
            w.CloseBlock();

            //Close file.
            w.CloseFile();

        }

        /// <summary>
        /// Export an SDK project.
        /// </summary>
        /// <param name="directory">The directory to export to.</param>
        /// <param name="projectName">The project name.</param>
        public void ExportSDKProject(string directory, string projectName) {

            //SBDL first.
            List<string> sbdl = new List<string>();         
            foreach (var e in Players) {
                sbdl.Add("#define " + e.Name + "\t" + e.Index);
            }
            foreach (var e in WaveArchives) {
                sbdl.Add("#define " + e.Name + "\t" + e.Index);
            }
            foreach (var e in StreamPlayers) {
                sbdl.Add("#define " + e.Name + "\t" + e.Index);
            }
            foreach (var e in Streams) {
                sbdl.Add("#define " + e.Name + "\t" + e.Index);
            }
            foreach (var e in Banks) {
                sbdl.Add("#define " + e.Name + "\t" + e.Index);
            }          
            foreach (var e in Sequences) {
                sbdl.Add("#define " + e.Name + "\t" + e.Index);
            }
            foreach (var e in SequenceArchives) {
                sbdl.Add("#define " + e.Name + "\t" + e.Index);
            }
            foreach (var e in Groups) {
                sbdl.Add("#define " + e.Name + "\t" + e.Index);
            }
            File.WriteAllLines(directory + "/" + projectName + ".sbdl", sbdl);

            //SPRJ.
            List<string> sprj = new List<string>();
            sprj.Add("<?xml version=\"1.0\"?>");
            sprj.Add("<NitroSoundMakerProject version=\"1.0.0\">");
            sprj.Add("  <head>");
            sprj.Add("    <create user=\"NitroStudio2User\" host=\"NitroStudio\" date=\"2020 - 3 - 18T12: 37:41\" />");
            sprj.Add("    <title>Nitro Studio 2 Export</title>");
            sprj.Add("    <generator name=\"cc\" version=\"1.2.0.0\" />");
            sprj.Add("  </head>");
            sprj.Add("  <body>");
            sprj.Add("    <SoundArchiveFiles>");
            sprj.Add("      <File name=\"" + projectName + "\" path=\"" + projectName + ".sarc\" />");
            sprj.Add("    </SoundArchiveFiles>");
            sprj.Add("  </body>");
            sprj.Add("</NitroSoundMakerProject>");
            File.WriteAllLines(directory + "/" + projectName + ".sprj", sprj);

            //Wave files.
            Dictionary<int, string> waveFiles = new Dictionary<int, string>();
            Dictionary<string, string> waveMd5sums = new Dictionary<string, string>();
            foreach (var e in WaveArchives) {

                //MD5SUM.
                string md5 = e.File.Md5Sum;

                //Unique.
                if (e.ForceIndividualFile) {
                    waveFiles.Add(e.Index, e.Name);
                    if (!waveMd5sums.ContainsKey(md5)) { waveMd5sums.Add(md5, e.Name); }
                }
                
                //Possibly shared.
                else {

                    //Already exists or not.
                    if (waveMd5sums.ContainsKey(md5)) {
                        waveFiles.Add(e.Index, waveMd5sums[md5]);
                    } else {
                        waveFiles.Add(e.Index, e.Name);
                        waveMd5sums.Add(md5, e.Name);
                    }

                }
            
            }

            //Stream files.
            Dictionary<int, string> strmFiles = new Dictionary<int, string>();
            Dictionary<string, string> strmMd5sums = new Dictionary<string, string>();
            foreach (var e in Streams) {

                //MD5SUM.
                string md5 = e.File.Md5Sum;

                //Unique.
                if (e.ForceIndividualFile) {
                    strmFiles.Add(e.Index, e.Name);
                    if (!strmMd5sums.ContainsKey(md5)) { strmMd5sums.Add(md5, e.Name); }
                }

                //Possibly shared.
                else {

                    //Already exists or not.
                    if (strmMd5sums.ContainsKey(md5)) {
                        strmFiles.Add(e.Index, strmMd5sums[md5]);
                    } else {
                        strmFiles.Add(e.Index, e.Name);
                        strmMd5sums.Add(md5, e.Name);
                    }

                }

            }

            //Bank files.
            Dictionary<int, string> bnkFiles = new Dictionary<int, string>();
            Dictionary<string, string> bnkMd5sums = new Dictionary<string, string>();
            foreach (var e in Banks) {

                //MD5SUM.
                string md5 = e.File.Md5Sum;

                //Unique.
                if (e.ForceIndividualFile) {
                    bnkFiles.Add(e.Index, e.Name);
                    if (!bnkMd5sums.ContainsKey(md5)) { bnkMd5sums.Add(md5, e.Name); }
                }

                //Possibly shared.
                else {

                    //Already exists or not.
                    if (bnkMd5sums.ContainsKey(md5)) {
                        bnkFiles.Add(e.Index, bnkMd5sums[md5]);
                    } else {
                        bnkFiles.Add(e.Index, e.Name);
                        bnkMd5sums.Add(md5, e.Name);
                    }

                }

            }

            //Sequence files.
            Dictionary<int, string> seqFiles = new Dictionary<int, string>();
            Dictionary<string, string> seqMd5sums = new Dictionary<string, string>();
            foreach (var e in Sequences) {

                //MD5SUM.
                string md5 = e.File.Md5Sum;

                //Unique.
                if (e.ForceIndividualFile) {
                    seqFiles.Add(e.Index, e.Name);
                    if (!seqMd5sums.ContainsKey(md5)) { seqMd5sums.Add(md5, e.Name); }
                }

                //Possibly shared.
                else {

                    //Already exists or not.
                    if (seqMd5sums.ContainsKey(md5)) {
                        seqFiles.Add(e.Index, seqMd5sums[md5]);
                    } else {
                        seqFiles.Add(e.Index, e.Name);
                        seqMd5sums.Add(md5, e.Name);
                    }

                }

            }

            //Sequence archive files.
            Dictionary<int, string> seqArcFiles = new Dictionary<int, string>();
            Dictionary<string, string> seqArcMd5sums = new Dictionary<string, string>();
            foreach (var e in SequenceArchives) {

                //MD5SUM.
                string md5 = e.File.Md5Sum;

                //Unique.
                if (e.ForceIndividualFile) {
                    seqArcFiles.Add(e.Index, e.Name);
                    if (!seqArcMd5sums.ContainsKey(md5)) { seqArcMd5sums.Add(md5, e.Name); }
                }

                //Possibly shared.
                else {

                    //Already exists or not.
                    if (seqArcMd5sums.ContainsKey(md5)) {
                        seqArcFiles.Add(e.Index, seqArcMd5sums[md5]);
                    } else {
                        seqArcFiles.Add(e.Index, e.Name);
                        seqArcMd5sums.Add(md5, e.Name);
                    }

                }

            }

            //SARC.
            List<string> sarc = new List<string>();

            //Dump players.
            int id = 0;
            sarc.Add("@PLAYER");
            foreach (var e in Players) {
                ushort bitFlags = e.BitFlags();
                if (bitFlags == 0xFFFF) {
                    bitFlags = 0;
                }
                string index = "";
                if (id != e.Index) {
                    id = e.Index;
                    index = "\t= " + e.Index;
                }
                id++;
                sarc.Add(e.Name + index + "\t: " + e.SequenceMax + ", " + e.HeapSize + ", 0x" + bitFlags.ToString("X"));
            }

            //Dump wave archives.
            id = 0;
            sarc.Add("\n@WAVEARC\n\n @PATH \"WaveArchives\"");
            foreach (var e in WaveArchives) {
                string index = "";
                if (id != e.Index) {
                    id = e.Index;
                    index = "\t= " + e.Index;
                }
                id++;
                sarc.Add(e.Name + index + "\t: TEXT, \"" + waveFiles[e.Index] + ".swls\"" + (e.LoadIndividually ? ", s" : ""));
            }

            //Dump banks.
            id = 0;
            sarc.Add("\n@BANK\n\n @PATH \"Banks\"");
            foreach (var e in Banks) {

                //Index.
                string index = "";
                if (id != e.Index) {
                    id = e.Index;
                    index = "\t= " + e.Index;
                }
                id++;

                //Text.
                bool text = true;
                try {
                    Directory.CreateDirectory("TEMP");
                    e.WriteTextFormat("TEMP", "Test");
                    Directory.Delete("TEMP", true);
                } catch { text = false; }

                //Stuff.
                string stuff = e.Name + index + "\t: " + (text ? "TEXT" : "BIN") + ", \"" + bnkFiles[e.Index] + "." + (text ? "" : "s") + "bnk\"" + ", ";

                //Wave archive names.
                string[] wars = new string[4];
                for (int i = 0; i < wars.Length; i++) {
                    if (e.WaveArchives[i] == null) {
                        switch (i) {
                            case 0:
                                if (e.ReadingWave0Id != 0xFFFF) {
                                    wars[i] = e.ReadingWave0Id.ToString();
                                }
                                break;
                            case 1:
                                if (e.ReadingWave1Id != 0xFFFF) {
                                    wars[i] = e.ReadingWave1Id.ToString();
                                }
                                break;
                            case 2:
                                if (e.ReadingWave2Id != 0xFFFF) {
                                    wars[i] = e.ReadingWave2Id.ToString();
                                }
                                break;
                            case 3:
                                if (e.ReadingWave3Id != 0xFFFF) {
                                    wars[i] = e.ReadingWave3Id.ToString();
                                }
                                break;
                        }
                    } else {
                        wars[i] = e.WaveArchives[i].Name;
                    }
                }

                if (wars[0] != null) {
                    stuff += wars[0];
                }
                if (wars[1] != null  || wars[2] != null || wars[3] != null) {
                    stuff += ", ";
                }

                if (wars[1] != null) {
                    stuff += wars[1];
                }
                if (wars[2] != null || wars[3] != null) {
                    stuff += ", ";
                }

                if (wars[2] != null) {
                    stuff += wars[2];
                }
                if (wars[3] != null) {
                    stuff += ", ";
                }

                if (wars[3] != null) {
                    stuff += wars[3];
                }

                //Add bank.
                sarc.Add(stuff);

            }

            //Sequences.
            id = 0;
            sarc.Add("\n@SEQ\n\n @PATH \"Sequences\"");
            foreach (var e in Sequences) {
                string index = "";
                if (id != e.Index) {
                    id = e.Index;
                    index = "\t= " + e.Index;
                }
                id++;
                sarc.Add(e.Name + index + "\t: TEXT, \"" + seqFiles[e.Index] + ".smft\", " + (e.Bank == null ? e.ReadingBankId.ToString() : e.Bank.Name) + ", " + e.Volume + ", " + e.ChannelPriority + ", " + e.PlayerPriority + ", " + (e.Player == null ? e.ReadingPlayerId.ToString() : e.Player.Name));
            }

            //Sequence archives.
            id = 0;
            sarc.Add("\n@SEQARC\n\n @PATH \"SequenceArchives\"");
            foreach (var e in SequenceArchives) {
                string index = "";
                if (id != e.Index) {
                    id = e.Index;
                    index = "\t= " + e.Index;
                }
                id++;
                sarc.Add(e.Name + index + "\t: TEXT, \"" + seqArcFiles[e.Index] + ".mus\"");
            }

            //Dump stream players.
            id = 0;
            sarc.Add("\n@STRM_PLAYER");
            foreach (var e in StreamPlayers) {
                string index = "";
                if (id != e.Index) {
                    id = e.Index;
                    index = "\t= " + e.Index;
                }
                id++;
                sarc.Add(e.Name + index + "\t: " + (e.IsStereo ? "STEREO" : "MONO") + ", " + e.LeftChannel + (e.IsStereo ? ", " + e.RightChannel : ""));
            }

            //Dump streams.
            id = 0;
            sarc.Add("\n@STRM\n\n @PATH \"Streams\"");
            foreach (var e in Streams) {
                string index = "";
                if (id != e.Index) {
                    id = e.Index;
                    index = "\t= " + e.Index;
                }
                id++;
                sarc.Add(e.Name + index + "\t: " + "STRM" + ", \"" + strmFiles[e.Index] + ".strm\", " + e.Volume + ", " + e.Priority + ", " + (e.Player == null ? e.ReadingPlayerId.ToString() : e.Player.Name));
            }

            //Dump groups.   
            sarc.Add("\n@GROUP");
            foreach (var e in Groups) {
                sarc.Add(e.Name + "\t:");
                foreach (var t in e.Entries) {
                    string stuff = "  ";
                    switch (t.Type) {
                        case GroupEntryType.Sequence:
                            stuff += (t.Entry as SequenceInfo).Name;
                            break;
                        case GroupEntryType.Bank:
                            stuff += (t.Entry as BankInfo).Name;
                            break;
                        case GroupEntryType.WaveArchive:
                            stuff += (t.Entry as WaveArchiveInfo).Name;
                            break;
                        case GroupEntryType.SequenceArchive:
                            stuff += (t.Entry as SequenceArchiveInfo).Name;
                            break;
                    }
                    bool sseq = t.LoadSequence, sbnk = t.LoadBank, swar = t.LoadWaveArchive;
                    switch (t.Type) {
                        case GroupEntryType.Sequence:
                            if (sseq && sbnk && swar) {

                            } else if (sbnk && swar) {
                                stuff += ", bw";
                            } else if (sseq && swar) {
                                stuff += ", sw";
                            } else if (swar) {
                                stuff += ", w";
                            } else if (sbnk) {
                                stuff += ", b";
                            } else if (sseq) {
                                stuff += ", s";
                            }
                            break;
                        case GroupEntryType.Bank:
                            if (sbnk && swar) {
                                stuff += ", bw";
                            } else if (swar) {
                                stuff += ", w";
                            } else if (sbnk) {
                                stuff += ", b";
                            }
                            break;

                    }
                    sarc.Add(stuff);
                }
                sarc.Add("");
            }

            //Save sarc.
            File.WriteAllLines(directory + "/" + projectName + ".sarc", sarc);

            //Write wave archives.
            List<string> wWavs = new List<string>();
            foreach (var e in WaveArchives) {
                Directory.CreateDirectory(directory + "/" + "WaveArchives");
                if (!wWavs.Contains(waveFiles[e.Index])) {
                    e.WriteTextFormat(directory + "/WaveArchives", waveFiles[e.Index]);
                    wWavs.Add(waveFiles[e.Index]);
                }
            }

            //Write streams.
            List<string> wStrms = new List<string>();
            foreach (var e in Streams) {
                Directory.CreateDirectory(directory + "/" + "Streams");
                if (!wStrms.Contains(strmFiles[e.Index])) {
                    e.File.Write(directory + "/" + "Streams" + "/" + strmFiles[e.Index] + ".strm");
                    RiffWave r = new RiffWave();
                    r.FromOtherStreamFile(e.File);
                    r.Write(directory + "/" + "Streams" + "/" + strmFiles[e.Index] + ".wav");
                    wStrms.Add(strmFiles[e.Index]);
                }
            }

            //Write sequences.
            List<string> wSeqs = new List<string>();
            foreach (var e in Sequences) {
                Directory.CreateDirectory(directory + "/" + "Sequences");
                if (!wSeqs.Contains(seqFiles[e.Index])) {
                    e.File.Name = seqFiles[e.Index];
                    e.File.ReadCommandData();
                    File.WriteAllLines(directory + "/" + "Sequences" + "/" + seqFiles[e.Index] + ".smft", e.File.ToText());
                    wSeqs.Add(seqFiles[e.Index]);
                }
            }

            //Write sequence archives.
            List<string> wSeqArcs = new List<string>();
            foreach (var e in SequenceArchives) {
                Directory.CreateDirectory(directory + "/" + "SequenceArchives");
                if (!wSeqArcs.Contains(seqArcFiles[e.Index])) {
                    e.File.Name = seqArcFiles[e.Index];
                    e.File.ReadCommandData(true);
                    var l = e.File.ToText().ToList();
                    l.Insert(0, "#include \"../" + projectName + ".sbdl\"\n");
                    File.WriteAllLines(directory + "/" + "SequenceArchives" + "/" + seqArcFiles[e.Index] + ".mus", l);
                    wSeqArcs.Add(seqArcFiles[e.Index]);
                }
            }

            //Write banks.
            List<string> wBnks = new List<string>();
            foreach (var e in Banks) {
                Directory.CreateDirectory(directory + "/" + "Banks");
                if (!wBnks.Contains(bnkFiles[e.Index])) {
                    try {
                        e.WriteTextFormat(directory + "/Banks", bnkFiles[e.Index]);
                        wBnks.Add(e.Name);
                    } catch {}
                }
            }

        }

    }

}
