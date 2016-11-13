using System.Collections.Generic;

namespace Base.Common
{
    public class DFError
    {
        public DFError()
        {
            HasError = false;
            HasImportError = false;
            LstError = new Dictionary<string, string>();
            LstSumError = new List<string>();
            LstData = new List<string>();
            LstErrImport = new List<DFErrorImport>();
        }

        public bool HasError { get; set; }
        public bool HasImportError { get; set; }

        public string Id { get; set; }
        public Dictionary<string, string> LstError { get; set; }
        public List<string> LstSumError { get; set; }
        public List<string> LstData { get; set; }
        public List<DFErrorImport> LstErrImport { get; set; }
    }

    public class DFErrorImport
    {
        public DFErrorImport(int rowIndex, string error)
        {
            RowIndex = rowIndex;
            Error = error;
        }
        public int RowIndex { set; get; }

        public string Error { set; get; }

    }

    public class DFResult
    {
        public bool Error { get; set; }
        public bool Suscess { get; set; }

        public string MSG { get; set; }

        public string LinkBack { get; set; }

        public string Data { get; set; }
    }
}