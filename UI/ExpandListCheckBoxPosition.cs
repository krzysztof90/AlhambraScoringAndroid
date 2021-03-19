namespace AlhambraScoringAndroid.UI
{
    public class ExpandListCheckBoxPosition : Java.Lang.Object
    {
        public int GroupPosition { get; set; }
        public int ChildPosition { get; set; }

        public ExpandListCheckBoxPosition(int groupPosition, int childPosition)
        {
            GroupPosition = groupPosition;
            ChildPosition = childPosition;
        }
    }
}