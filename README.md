Overview
This project predicts NYC taxi trip durations using a regression model. The code is written in C# using ML.NET, and the application can be run using Visual Studio Code (VS Code).

How to Run the Application
1. Clone the Repository:
git clone <repository-url>
cd <repository-folder>
2. Open in VS Code:
Open the cloned repository folder in Visual Studio Code.
3. Run the Application:
Open the terminal in VS Code and run:
dotnet run

This command will automatically install all necessary NuGet packages and build the application. The initial build might take longer due to the complex training of the taxi model. Subsequent builds should be faster as the model will be saved to a specified location.
Both datasets are included in the repository, given that this is a test task, for easier application execution. The same applies to model training, which will automatically start when the application is run, so the endpoints can be used immediately after the build.

4. Testing the Endpoint:
The simplest way to test the endpoint is through Swagger. Open a browser and navigate to:
http://localhost:5113/swagger/index.html

Model Performance
The model for Boston Housing has a significantly higher R² value and is more accurate.
The model for NYC Taxi, despite using detailed methods, is less accurate. This is likely due to the need for advanced preprocessing and data processing, which I was unable to investigate now and implement due to time constraints.
Both endpoints return reasonable values, although the NYC model has notable shortcomings.

You can use data in this format to test endpoints:
NYC Taxi:
{
        "vendor_id": 1,
        "passenger_count": 1,
        "pickup_longitude": -73.9876,
        "pickup_latitude": 40.7661,
        "dropoff_longitude": -73.9820,
        "dropoff_latitude": 40.7681,
        "store_and_fwd_flag": 0    
}

Boston Housing:
{
    "crim": 0.02729,
    "zn": 0.0,
    "indus": 7.07,
    "chas": 0,
    "nox": 0.469,
    "rm": 7.185,
    "age": 61.1,
    "dis": 4.9671,
    "rad": 2,
    "tax": 242,
    "ptratio": 17.8,
    "b": 392.83,
    "lstat": 4.03
}

Scaling the Solution

1. Scaling for Large Datasets (Training):

- Distributed Training: Utilize distributed computing frameworks such as Apache Spark or Dask to distribute the training workload across multiple nodes.
- Cloud Services: Leverage cloud-based machine learning services like Azure Machine Learning or AWS SageMaker, which offer managed infrastructure for handling large datasets.
- Data Partitioning: Partition the data into smaller chunks and perform mini-batch training, which can help in managing memory usage and speeding up the training process.

2. Scaling for Large Datasets (APIs):

- Efficient Data Storage: Store the data in a scalable cloud storage solution such as Azure Blob Storage or AWS S3, which can handle large volumes of data efficiently.
- Data Chunking: Split the data into smaller chunks and process them in parallel. This can be achieved by implementing data chunking and parallel processing techniques.
- Asynchronous Processing: Use asynchronous processing to handle data loading and processing tasks in the background, reducing the load on the API.
- Caching: Implement caching mechanisms to store frequently accessed data, reducing the need for repeated data retrieval from the storage.
- Optimize API Endpoints: Optimize API endpoints to handle data retrieval efficiently.

3. Scaling for a Large Number of API Calls:

- Load Balancing: Implement load balancers to distribute incoming traffic across multiple server instances, ensuring no single server is overwhelmed.
- Horizontal Scaling: Scale out the application by adding more server instances, allowing the system to handle more concurrent API requests.
- Rate Limiting: Implement rate limiting to prevent abuse and ensure fair usage of the API, protecting the system from being overwhelmed by too many requests.
- Asynchronous Processing: Use asynchronous processing and queuing mechanisms (e.g., RabbitMQ, Kafka) to handle long-running tasks, freeing up resources for handling more API calls.