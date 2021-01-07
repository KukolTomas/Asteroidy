using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft;
using System.Net.Http;
using System.Net;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Asteroidy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        async private void button1_Click(object sender, EventArgs e)
        {
            //string baseUrl = "https://api.nasa.gov/neo/rest/v1/feed?start_date=2015-09-07&end_date=2015-09-08&api_key=nyAIM113ny1b6UcavangnlRLsxVejWFDn5rDaL1F";
            //try
            //{
            //    using(HttpClient client = new HttpClient())
            //    {
            //    using(HttpResponseMessage res = await client.GetAsync(baseUrl))
            //        {
            //            using (HttpContent content = res.Content)
            //            {
            //                var data = await content.ReadAsStringAsync();
            //                JObject o = JObject.Parse(data);
            //                textBox1.Text = data;
            //            }
            //        }
            //    }
            //}
            //catch (Exception )
            //{

            //    textBox1.Text = "Výjimka!";
            //}
            ////WebRequest request = HttpWebRequest.Create("https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=nyAIM113ny1b6UcavangnlRLsxVejWFDn5rDaL1F");
            ////WebResponse response = request.GetResponse();
            ////StreamReader sr = new StreamReader(response.GetResponseStream());
            var json = await BasicCallAsync();
            textBox1.Text = "";
            foreach (var item in json.near_earth_objects)
            {
                textBox1.Text += string.Format("{0},{1},{2}km \r\n", item.id, item.name, item.close_approach_data[0].miss_distance.kilometers);
            }
        }
        private static async Task<Root> BasicCallAsync()
        {
            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync("https://api.nasa.gov/neo/rest/v1/neo/browse?api_key=nyAIM113ny1b6UcavangnlRLsxVejWFDn5rDaL1F");
                return JsonConvert.DeserializeObject<Root>(content);
            }
        }
    }

    public class Links
    {
        public string next { get; set; }
        public string self { get; set; }
    }

    public class Page
    {
        public int size { get; set; }
        public int total_elements { get; set; }
        public int total_pages { get; set; }
        public int number { get; set; }
    }

    public class Kilometers
    {
        public double estimated_diameter_min { get; set; }
        public double estimated_diameter_max { get; set; }
    }

    public class Meters
    {
        public double estimated_diameter_min { get; set; }
        public double estimated_diameter_max { get; set; }
    }

    public class Miles
    {
        public double estimated_diameter_min { get; set; }
        public double estimated_diameter_max { get; set; }
    }

    public class Feet
    {
        public double estimated_diameter_min { get; set; }
        public double estimated_diameter_max { get; set; }
    }

    public class EstimatedDiameter
    {
        public Kilometers kilometers { get; set; }
        public Meters meters { get; set; }
        public Miles miles { get; set; }
        public Feet feet { get; set; }
    }

    public class RelativeVelocity
    {
        public string kilometers_per_second { get; set; }
        public string kilometers_per_hour { get; set; }
        public string miles_per_hour { get; set; }
    }

    public class MissDistance
    {
        public string astronomical { get; set; }
        public string lunar { get; set; }
        public string kilometers { get; set; }
        public string miles { get; set; }
    }

    public class CloseApproachData
    {
        public string close_approach_date { get; set; }
        public string close_approach_date_full { get; set; }
        public object epoch_date_close_approach { get; set; }
        public RelativeVelocity relative_velocity { get; set; }
        public MissDistance miss_distance { get; set; }
        public string orbiting_body { get; set; }
    }

    public class OrbitClass
    {
        public string orbit_class_type { get; set; }
        public string orbit_class_description { get; set; }
        public string orbit_class_range { get; set; }
    }

    public class OrbitalData
    {
        public string orbit_id { get; set; }
        public string orbit_determination_date { get; set; }
        public string first_observation_date { get; set; }
        public string last_observation_date { get; set; }
        public int data_arc_in_days { get; set; }
        public int observations_used { get; set; }
        public string orbit_uncertainty { get; set; }
        public string minimum_orbit_intersection { get; set; }
        public string jupiter_tisserand_invariant { get; set; }
        public string epoch_osculation { get; set; }
        public string eccentricity { get; set; }
        public string semi_major_axis { get; set; }
        public string inclination { get; set; }
        public string ascending_node_longitude { get; set; }
        public string orbital_period { get; set; }
        public string perihelion_distance { get; set; }
        public string perihelion_argument { get; set; }
        public string aphelion_distance { get; set; }
        public string perihelion_time { get; set; }
        public string mean_anomaly { get; set; }
        public string mean_motion { get; set; }
        public string equinox { get; set; }
        public OrbitClass orbit_class { get; set; }
    }

    public class NearEarthObject
    {
        public Links links { get; set; }
        public string id { get; set; }
        public string neo_reference_id { get; set; }
        public string name { get; set; }
        public string name_limited { get; set; }
        public string designation { get; set; }
        public string nasa_jpl_url { get; set; }
        public double absolute_magnitude_h { get; set; }
        public EstimatedDiameter estimated_diameter { get; set; }
        public bool is_potentially_hazardous_asteroid { get; set; }
        public List<CloseApproachData> close_approach_data { get; set; }
        public OrbitalData orbital_data { get; set; }
        public bool is_sentry_object { get; set; }
    }

    public class Root
    {
        public Links links { get; set; }
        public Page page { get; set; }
        public List<NearEarthObject> near_earth_objects { get; set; }
    }


}

