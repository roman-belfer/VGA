using System;

namespace Common.Infrastructure.DataModels
{
    [Serializable]
    public class ValueModel
    {
        public uint ID { get; set; }
        public string Value { get; set; }

        public ValueModel(uint id, string value)
        {
            ID = id;
            Value = value;
        }
    }
}
