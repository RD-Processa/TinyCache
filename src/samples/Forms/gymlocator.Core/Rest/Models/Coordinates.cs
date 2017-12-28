// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace gymlocator.Rest.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Longitude and latitude coordinates for a position of a gym.
    /// </summary>
    public partial class Coordinates
    {
        /// <summary>
        /// Initializes a new instance of the Coordinates class.
        /// </summary>
        public Coordinates()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Coordinates class.
        /// </summary>
        /// <param name="lng">Longitude</param>
        /// <param name="lat">Latitude</param>
        public Coordinates(double lng, double lat)
        {
            Lng = lng;
            Lat = lat;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets longitude
        /// </summary>
        [JsonProperty(PropertyName = "lng")]
        public double Lng { get; set; }

        /// <summary>
        /// Gets or sets latitude
        /// </summary>
        [JsonProperty(PropertyName = "lat")]
        public double Lat { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            //Nothing to validate
        }
    }
}