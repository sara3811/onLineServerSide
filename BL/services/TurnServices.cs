using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DAL;

namespace BL.services
{
    static public  class TurnServices
    {

        public static int GooglePlaces(string startLongitude, string startLatitude,string destination,bool isDriving)
        {
            string mode="walking";
            if (isDriving)
                mode = "driving";
            
            string uri = "https://maps.googleapis.com/maps/api/distancematrix/xml?key=AIzaSyA5L81_-5d2Hy7hHsNVhodk1zS90Qu-aP8&origins="
                          + startLatitude+","+startLongitude + "&destinations=" + destination + "&mode="+mode +"&units=imperial&sensor=false";
            WebClient wc = new WebClient();
            try
            {
                string geoCodeInfo = wc.DownloadString(uri);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(geoCodeInfo);
                string duration = xmlDoc.DocumentElement.SelectSingleNode("/DistanceMatrixResponse/row/element/duration/value").InnerText;
                return Convert.ToInt32(duration) / 60;
            }
            catch (Exception)
            {
                return -1;
            }
        }



    }
}
