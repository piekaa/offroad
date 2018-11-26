using System;

namespace Pieka.Data
{
    [Serializable]
    public class TestData
    {
        public string A = "Simea";
        public int B = 10;
        public float C = 1.23f;
        public Inner I = new Inner();
    }
}