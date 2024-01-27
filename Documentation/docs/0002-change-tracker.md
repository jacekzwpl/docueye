## Change Tracker
Change tracker logs changes on model elements and relationships.  
The identifier that is used for comparison is `structurizr.dsl.identifier`.  
If You want to change tracker works as expected, you should use identifiers in Your dsl code for every element and relationship. If You don't then every time You export workspace to json elements and relationships will have unique identifiers and change tracker will threat them as new.  
Example for using identifiers: 
```
mysystem1 = softwareSystem "Software System 1"
mysystem2 = softwareSystem "Software System 2"

relation = mysystem1 -> mysystem2
```
