# BookStore

This app uses CQRS pattern that has an Azure CosmosDB to read current state of book stock and a MS Sql to write book movement events
##
You need CosmosDB connection string and SQL connection string to run this app in development environment.
#
Event database, an event records book id, quantity, stock destination, date and time
![image](https://user-images.githubusercontent.com/24510302/219948684-f09fbdb1-cdae-4313-8d00-9703f647cf8d.png)
#
CurrentState database, each stock have a document. Users can reserve a book that moves a book from store stock to user stock.
![image](https://user-images.githubusercontent.com/24510302/219948810-54e432b7-3324-4596-a838-c565220e9e63.png)
