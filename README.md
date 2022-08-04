# Persönliche Homepage

1. Homepage.Web <br>
ist ein ASP .NET C# Model-View-Controller Projekt und zuständig für die Darstellung und Visualisierung der 
Webseite. Alle Benutzerinteraktionen laufen darüber.

2. Homepage.GlobalDefinitions
ist eine .NET Klassenbibliothek. In diesem Projekt sind mehrere Klassen enthalten die Properties beinhalten.

3. Homepage.Backend
ist eine .NET Klassenbibliothek. In diesem Projekt wird unterschieden zwischen Business Logik und Datenzugriffen.
Die Homepage.GlobalDefinitions DLL ist auf das Homepage.Backend Projet verwiesen. 

4. Homepage.REST
ist eine ASP .NET C# Web-API. Dieses Projekt bereitet benötigte Daten vor die aus der Homepage.Backend DLL kommen.

5. Homepage.Web.RESTCaller
ist eine .NET Klassenbibliothek. Das Projekt ist verwiesen auf das Homepage.Web Projekt. Durch die RESTCaller DLL
kann man die Web-API aufrufen und Daten abrufen und auf der Webseite darstellen.
