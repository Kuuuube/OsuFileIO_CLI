using OsuFileIO.Analyzer;
using OsuFileIO.HitObject.OsuStd;
using OsuFileIO.OsuFile;
using OsuFileIO.OsuFileReader;

namespace CollectionCSVtoDB
{
    class Program
    {
        public static string GetMD5Checksum(string filename)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "");
                }
            }
        }

        static void Main(string[] args)
        {
            string input;
            string output = "";

            if (args.Length != 0)
            {
                input = args[0];
                output = args[1];
            }
            else
            {
                Console.WriteLine("Enter Input Path:");
                input = Console.ReadLine();
                Console.WriteLine("Enter Output Path (leave blank to use checksum):");
                output = Console.ReadLine();
            }
            var path = input;

            using var reader = new OsuFileReaderBuilder(path).Build();
            var beatmap = reader.ReadFile();

            if (beatmap is IReadOnlyBeatmap<StdHitObject> stdBeatmap)
            {
                var result = stdBeatmap.Analyze();

                var checksum = GetMD5Checksum(input);

                string checksum_format = "{" + "\"checksum\":\"" + checksum + "\",";
                string analysis_header = "\"analysis\":" + "[{";

                string CircleSize = "\"CircleSize\":\"" + "\",";
                if (stdBeatmap.Difficulty.CircleSize != null)
                {
                    CircleSize = "\"CircleSize\":\"" + stdBeatmap.Difficulty.CircleSize.ToString() + "\"" + ",";
                }
                string HPDrainRate = "\"HPDrainRate\":\"" + "\",";
                if (stdBeatmap.Difficulty.HPDrainRate != null)
                {
                    HPDrainRate = "\"HPDrainRate\":\"" + stdBeatmap.Difficulty.HPDrainRate.ToString() + "\"" + ",";
                }
                string OverallDifficulty = "\"OverallDifficulty\":\"" + "\",";
                if (stdBeatmap.Difficulty.OverallDifficulty != null)
                {
                    OverallDifficulty = "\"OverallDifficulty\":\"" + stdBeatmap.Difficulty.OverallDifficulty.ToString() + "\"" + ",";
                }
                string ApproachRate = "\"ApproachRate\":\"" + "\",";
                if (stdBeatmap.Difficulty.ApproachRate != null)
                {
                    ApproachRate = "\"ApproachRate\":\"" + stdBeatmap.Difficulty.ApproachRate.ToString() + "\"" + ",";
                }
                string SliderMultiplier = "\"SliderMultiplier\":\"" + "\",";
                if (stdBeatmap.Difficulty.SliderMultiplier != null)
                {
                    SliderMultiplier = "\"SliderMultiplier\":\"" + stdBeatmap.Difficulty.SliderMultiplier.ToString() + "\"" + ",";
                }
                string SliderTickRate = "\"SliderTickRate\":\"" + "\",";
                if (stdBeatmap.Difficulty.SliderTickRate != null)
                {
                    SliderTickRate = "\"SliderTickRate\":\"" + stdBeatmap.Difficulty.SliderTickRate.ToString() + "\"" + ",";
                }
                string MetaDataArtist = "\"MetaDataArtist\":\"" + "\",";
                if (stdBeatmap.MetaData.Artist != null)
                {
                    MetaDataArtist = "\"MetaDataArtist\":\"" + stdBeatmap.MetaData.Artist.ToString() + "\"" + ",";
                }
                string MetaDataArtistUnicode = "\"MetaDataArtistUnicode\":\"" + "\",";
                if (stdBeatmap.MetaData.ArtistUnicode != null)
                {
                    MetaDataArtistUnicode = "\"MetaDataArtistUnicode\":\"" + stdBeatmap.MetaData.ArtistUnicode.ToString() + "\"" + ",";
                }
                string MetaDataBeatmapID = "\"MetaDataBeatmapID\":\"" + "\",";
                if (stdBeatmap.MetaData.BeatmapID != null)
                {
                    MetaDataBeatmapID = "\"MetaDataBeatmapID\":\"" + stdBeatmap.MetaData.BeatmapID.ToString() + "\"" + ",";
                }
                string MetaDataBeatmapSetID = "\"MetaDataBeatmapSetID\":\"" + "\",";
                if (stdBeatmap.MetaData.BeatmapSetID != null)
                {
                    MetaDataBeatmapSetID = "\"MetaDataBeatmapSetID\":\"" + stdBeatmap.MetaData.BeatmapSetID.ToString() + "\"" + ",";
                }
                string MetaDataCreator = "\"MetaDataCreator\":\"" + "\",";
                if (stdBeatmap.MetaData.Creator != null)
                {
                    MetaDataCreator = "\"MetaDataCreator\":\"" + stdBeatmap.MetaData.Creator.ToString() + "\"" + ",";
                }
                string MetaDataSource = "\"MetaDataSource\":\"" + "\",";
                if (stdBeatmap.MetaData.Source != null)
                {
                    MetaDataSource = "\"MetaDataSource\":\"" + stdBeatmap.MetaData.Source.ToString() + "\"" + ",";
                }
                string MetaDataTitle = "\"MetaDataTitle\":\"" + "\",";
                if (stdBeatmap.MetaData.Title != null)
                {
                    MetaDataTitle = "\"MetaDataTitle\":\"" + stdBeatmap.MetaData.Title.ToString() + "\"" + ",";
                }
                string MetaDataTitleUnicode = "\"MetaDataTitleUnicode\":\"" + "\",";
                if (stdBeatmap.MetaData.TitleUnicode != null)
                {
                    MetaDataTitleUnicode = "\"MetaDataTitleUnicode\":\"" + stdBeatmap.MetaData.TitleUnicode.ToString() + "\"" + ",";
                }
                string MetaDataVersion = "\"MetaDataVersion\":\"" + "\",";
                if (stdBeatmap.MetaData.Version != null)
                {
                    MetaDataVersion = "\"MetaDataVersion\":\"" + stdBeatmap.MetaData.Version.ToString() + "\"" + ",";
                }

                string HitObjectsCount = "\"HitObjectsCount\":\"" + stdBeatmap.HitObjects.Count.ToString() + "\"" + ",";

                string HitCircleCount = "\"HitCircleCount\":\"" + result.HitCircleCount.ToString() + "\"" + ",";
                string SliderCount = "\"SliderCount\":\"" + result.SliderCount.ToString() + "\"" + ",";
                string SpinnerCount = "\"SpinnerCount\":\"" + result.SpinnerCount.ToString() + "\"" + ",";

                string Length = "\"Length\":\"" + result.Length.ToString() + "\"" + ",";
                string Bpm = "\"Bpm\":\"" + result.Bpm.ToString() + "\"" + ",";
                string BpmMax = "\"BpmMax\":\"" + result.BpmMax.ToString() + "\"" + ",";
                string BpmMin = "\"BpmMin\":\"" + result.BpmMin.ToString() + "\"" + ",";

                string TotalSliderLength = "\"TotalSliderLength\":\"" + result.TotalSliderLength.ToString() + "\"" + ",";
                string AvgFasterSliderSpeed = "\"AvgFasterSliderSpeed\":\"" + result.AvgFasterSliderSpeed.ToString() + "\"" + ",";
                string SliderSpeedDifference = "\"SliderSpeedDifference\":\"" + result.SliderSpeedDifference.ToString() + "\"" + ",";
                string BèzierSliderCount = "\"BèzierSliderCount\":\"" + result.BèzierSliderCount.ToString() + "\"" + ",";
                string CatmullSliderCount = "\"CatmullSliderCount\":\"" + result.CatmullSliderCount.ToString() + "\"" + ",";
                string KickSliderCount = "\"KickSliderCount\":\"" + result.KickSliderCount.ToString() + "\"" + ",";
                string LinearSliderCount = "\"LinearSliderCount\":\"" + result.LinearSliderCount.ToString() + "\"" + ",";
                string SliderPointCount = "\"SliderPointCount\":\"" + result.SliderPointCount.ToString() + "\"" + ",";
                string AvgSliderPointCount = "\"AvgSliderPointCount\":\"" + result.AvgSliderPointCount.ToString() + "\"" + ",";
                string PerfectCicleSliderCount = "\"PerfectCicleSliderCount\":\"" + result.PerfectCicleSliderCount.ToString() + "\"" + ",";
                string SliderPerfectStackCount = "\"SliderPerfectStackCount\":\"" + result.SliderPerfectStackCount.ToString() + "\"" + ",";

                string TotalJumpPixels = "\"TotalJumpPixels\":\"" + result.TotalJumpPixels.ToString() + "\"" + ",";
                string CrossScreenJumpCount = "\"CrossScreenJumpCount\":\"" + result.CrossScreenJumpCount.ToString() + "\"" + ",";
                string Jump180DegreesCount = "\"Jump180DegreesCount\":\"" + result.Jump180DegreesCount.ToString() + "\"" + ",";
                string Jump90DegreesCount = "\"Jump90DegreesCount\":\"" + result.Jump90DegreesCount.ToString() + "\"" + ",";

                string BurstCount = "\"BurstCount\":\"" + result.BurstCount.ToString() + "\"" + ",";
                string DoubleCount = "\"DoubleCount\":\"" + result.DoubleCount.ToString() + "\"" + ",";
                string TripletCount = "\"TripletCount\":\"" + result.TripletCount.ToString() + "\"" + ",";
                string QuadrupletCount = "\"QuadrupletCount\":\"" + result.QuadrupletCount.ToString() + "\"" + ",";
                string StandaloneDoubleCount = "\"StandaloneDoubleCount\":\"" + result.StandaloneDoubleCount.ToString() + "\"" + ",";
                string StandaloneQuadrupletCount = "\"StandaloneQuadrupletCount\":\"" + result.StandaloneQuadrupletCount.ToString() + "\"" + ",";
                string StandaloneTripletCount = "\"StandaloneTripletCount\":\"" + result.StandaloneTripletCount.ToString() + "\"" + ",";
                string CirclePerfectStackCount = "\"CirclePerfectStackCount\":\"" + result.CirclePerfectStackCount.ToString() + "\"" + ",";

                string StreamCount = "\"StreamCount\":\"" + result.StreamCount.ToString() + "\"" + ",";
                string StreamCutsCount = "\"StreamCutsCount\":\"" + result.StreamCutsCount.ToString() + "\"" + ",";
                string LongestStream = "\"LongestStream\":\"" + result.LongestStream.ToString() + "\"" + ",";
                string LongStreamCount = "\"LongStreamCount\":\"" + result.LongStreamCount.ToString() + "\"" + ",";
                string SlidersInStreamAlike = "\"SlidersInStreamAlike\":\"" + result.SlidersInStreamAlike.ToString() + "\"" + ",";
                string DeathStreamCount = "\"DeathStreamCount\":\"" + result.DeathStreamCount.ToString() + "\"" + ",";
                string TotalSpacedStreamAlikePixels = "\"TotalSpacedStreamAlikePixels\":\"" + result.TotalSpacedStreamAlikePixels.ToString() + "\"" + ",";
                string TotalStreamAlikePixels = "\"TotalStreamAlikePixels\":\"" + result.TotalStreamAlikePixels.ToString() + "\"" + ",";

                string UniqueDistancesCount = "\"UniqueDistancesCount\":\"" + result.UniqueDistancesCount.ToString() + "\"";

                string analysis_footer = "}]}";


                string analysis_string = checksum_format + analysis_header + CircleSize + HPDrainRate + OverallDifficulty + ApproachRate + SliderMultiplier + SliderTickRate + MetaDataArtist + MetaDataArtistUnicode + MetaDataBeatmapID + MetaDataBeatmapSetID + MetaDataCreator + MetaDataSource + MetaDataTitle + MetaDataTitleUnicode + MetaDataVersion + HitObjectsCount + HitCircleCount + SliderCount + SpinnerCount + Length + Bpm + BpmMax + BpmMin + TotalSliderLength + AvgFasterSliderSpeed + SliderSpeedDifference + BèzierSliderCount + CatmullSliderCount + KickSliderCount + LinearSliderCount + SliderPointCount + AvgSliderPointCount + PerfectCicleSliderCount + SliderPerfectStackCount + TotalJumpPixels + CrossScreenJumpCount + Jump180DegreesCount + Jump90DegreesCount + BurstCount + DoubleCount + TripletCount + QuadrupletCount + StandaloneDoubleCount + StandaloneQuadrupletCount + StandaloneTripletCount + CirclePerfectStackCount + StreamCount + StreamCutsCount + LongestStream + LongStreamCount + SlidersInStreamAlike + DeathStreamCount + TotalSpacedStreamAlikePixels + TotalStreamAlikePixels + UniqueDistancesCount + analysis_footer;

                string[] analysis = { analysis_string };

                if (output == "")
                {
                    File.WriteAllLines(checksum + ".json", analysis);
                }
                else if (output == "checksum")
                {
                    File.WriteAllLines(checksum + ".json", analysis);
                }
                else
                {
                    File.WriteAllLines(output, analysis);
                }
            }
        }
    }
}