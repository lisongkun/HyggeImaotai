namespace HyggeIMaoTai.Entity
{
    public class MapPoint
    {
        #region Properties

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        #endregion

        #region Construct

        public MapPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public MapPoint()
        {
        }

        #endregion
    }
}
