using Common.Infrastructure.Enums;

namespace Common.Infrastructure.DataModels
{
    public class EditModel
    {
        public EditModel(EditModeEnum editMode)
        {
            EditMode = editMode;
        }

        public EditModel(uint id, EditModeEnum editMode)
        {
            ID = id;
            EditMode = editMode;
        }

        public uint ID { get; set; }
        public EditModeEnum EditMode { get; set; }
    }
}
