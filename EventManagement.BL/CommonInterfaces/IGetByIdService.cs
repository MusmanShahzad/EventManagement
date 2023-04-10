namespace EventManagement.BL.CommonInterfaces
{
        using System.Collections.Generic;
        using System.Threading.Tasks;

        /// <summary>
        /// Defines the contract for the Get rest api endpoint.
        /// </summary>
        /// <typeparam name="TDataSource">The type of the data source.</typeparam>
        /// <typeparam name="TOutputData">The type of the output data.</typeparam>
        public interface IGetByIdService<TOutputData>
            where TOutputData : new()
        {

            /// <summary>
            /// Gets or sets the transformed data set.
            /// </summary>
            TOutputData Data { get; set; }

            /// <summary>
            /// Extracts data out of the data model, ready to be transformed.
            /// </summary>
            /// <returns>An asynchronous task.</returns>
            Task ExtractAsync(Guid Id);

            /// <summary>
            /// Transforms data from the data model ready to load.
            /// </summary>
            /// <returns>An asynchronous task.</returns>
            void TransformAsync();

            /// <summary>
            /// Loads staged data into the final format to be returned to the front end.
            /// </summary>
            /// <returns>An asynchronous task.</returns>
            Task<TOutputData> LoadAsync();
        }
    }
