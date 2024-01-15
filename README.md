# Azure Queue Message Processing

![Project Banner](/media/project-banner.png)

Hi, this is a project where I explore Azure Storage Account services, more precisely, Azure Queues.

I created 3 REST APIS:
- ParserAPI;
    - Parses a .XML file into a Transfer object.
- ReaderAPI;
    - Grabs all files in a specific folder, iterates them and parses them calling ParserAPI;
    - After each successful iteration deletes the parsed file;
- ProcessorAPI.
    - Inserts a Transfer object into a MariaDB Database.

And an Azure Functions project, containing 2 time triggers:
- Uploader Function;
    - From time to time, calls ReaderAPI, doing the previously mentioned things;
    - Iterates the retrieved list and tries to upload each Transfer object to the Azure Queues.
- Processor Function.
    - Grabs at least a defined number of Transfer objects in the Azure Queue, and iterates them;
    - For each one, tries to call ProcessorAPI and passes the object to be Inserted into the database;
    - After a successful insertion, deletes the message from the Queue.

I have here a diagram that shows better how this project works:

![Project Diagram](/media/project-diagram.png)

You can also see this video that I recorded that shows this working:

[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/hZ0GALstjuQ/0.jpg)](https://www.youtube.com/watch?v=hZ0GALstjuQ)

Hope you enjoy! If you are interested in discussing any idea, you can contact me through my [portfolio](https://www.eduardobagarrao.com) or [email me](mailto:general@eduardobagarrao.com)! 
