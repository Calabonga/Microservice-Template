using System;

namespace $safeprojectname$.Infrastructure.Attributes
{
    /// <summary>
    /// Custom attribute for Swagger Upload Form
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SwaggerFormAttribute : Attribute
    {
        /// <inheritdoc />
        public SwaggerFormAttribute(string parameterName, string description, bool hasFileUpload = true)
        {
            ParameterName = parameterName;
            Description = description;
            HasFileUpload = hasFileUpload;
        }

        /// <summary>
        /// UploadFile enabled
        /// </summary>
        public bool HasFileUpload { get; }

        /// <summary>
        /// Name for the parameter <see cref="T:IFormFile"/>
        /// </summary>
        public string ParameterName { get; }

        /// <summary>
        /// Small description
        /// </summary>
        public string Description { get;}
    }
}