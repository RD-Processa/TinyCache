// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace gymlocator.Core.Shopping.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class ShoppingList
    {
        /// <summary>
        /// Initializes a new instance of the ShoppingList class.
        /// </summary>
        public ShoppingList()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ShoppingList class.
        /// </summary>
        public ShoppingList(int id, System.DateTime created, bool completed, System.DateTime done, string name, int storeID)
        {
            Id = id;
            Created = created;
            Completed = completed;
            Done = done;
            Name = name;
            StoreID = storeID;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "created")]
        public System.DateTime Created { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "completed")]
        public bool Completed { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "done")]
        public System.DateTime Done { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "storeID")]
        public int StoreID { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Name == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Name");
            }
        }
    }
}
