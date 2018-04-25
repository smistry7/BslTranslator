using System.IO;

namespace RandomForestTranslator
{
    public static class FileLocations
    {
        public static string TwoHandedModelFilePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName
                                                      + @"\Models\updatedRandomForest.model";
        public static string OneHandedModelFilePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName
                                                      + @"\Models\updatedLogistic.model";
        public static string OneHandedDataFilePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName
                                                     + @"\DataSets\SingleHandData.arff";
        public static string TwoHandedDataFilePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName
                                                     + @"\DataSets\SignLanguageDataUpdateable.arff";

        public static string TwoHandedProgramData =
            Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName
            + @"\DataSets\SignLanguageProgramData.arff";
        public static string OneHandedProgramData = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName
                                                    + @"\DataSets\SingleHandProgramData.arff";
    }
}