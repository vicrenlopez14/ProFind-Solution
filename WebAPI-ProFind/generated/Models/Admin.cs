// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace WebAPI.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    public partial class Admin
    {
        /// <summary>
        /// Initializes a new instance of the Admin class.
        /// </summary>
        public Admin()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Admin class.
        /// </summary>
        public Admin(string idA = default(string), string nameA = default(string), string emailA = default(string), string telA = default(string), string passwordA = default(string), byte[] pictureA = default(byte[]), string idR1 = default(string))
        {
            IdA = idA;
            NameA = nameA;
            EmailA = emailA;
            TelA = telA;
            PasswordA = passwordA;
            PictureA = pictureA;
            IdR1 = idR1;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "idA")]
        public string IdA { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "nameA")]
        public string NameA { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "emailA")]
        public string EmailA { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "telA")]
        public string TelA { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "passwordA")]
        public string PasswordA { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "pictureA")]
        public byte[] PictureA { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "idR1")]
        public string IdR1 { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (IdA != null)
            {
                if (IdA.Length > 21)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "IdA", 21);
                }
                if (IdA.Length < 0)
                {
                    throw new ValidationException(ValidationRules.MinLength, "IdA", 0);
                }
            }
            if (NameA != null)
            {
                if (NameA.Length > 50)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "NameA", 50);
                }
                if (NameA.Length < 0)
                {
                    throw new ValidationException(ValidationRules.MinLength, "NameA", 0);
                }
            }
            if (EmailA != null)
            {
                if (EmailA.Length > 50)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "EmailA", 50);
                }
                if (EmailA.Length < 0)
                {
                    throw new ValidationException(ValidationRules.MinLength, "EmailA", 0);
                }
            }
            if (TelA != null)
            {
                if (TelA.Length > 15)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "TelA", 15);
                }
                if (TelA.Length < 0)
                {
                    throw new ValidationException(ValidationRules.MinLength, "TelA", 0);
                }
            }
            if (PasswordA != null)
            {
                if (PasswordA.Length > 64)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "PasswordA", 64);
                }
                if (PasswordA.Length < 0)
                {
                    throw new ValidationException(ValidationRules.MinLength, "PasswordA", 0);
                }
            }
            if (IdR1 != null)
            {
                if (IdR1.Length > 21)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "IdR1", 21);
                }
                if (IdR1.Length < 0)
                {
                    throw new ValidationException(ValidationRules.MinLength, "IdR1", 0);
                }
            }
        }
    }
}
