# Deltatre
Deltatre Code
We would like to expose a RESTful API that allows a user to perform the following operations: 
•	Search for a single word 
•	Update the list of searchable words 
•	Return the top 5 search keywords and how many times each has been searched 
The system should be split into two services: 
•	Front-end/Gateway service: Rest API 
•	Back-end service 
Communication between the two services ideally would be using GRPC based however this is not a strict requirement. 
•	All API operations should be case insensitive 
•	Updated data does not need to be persisted between restarts, but we don’t mind if it does.
