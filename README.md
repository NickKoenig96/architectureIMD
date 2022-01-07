# Architecture: events app

We are responsible for the creation, and later maintenance of the **API** of a greenfield web/mobile application. Since the tech-stack of our company is based on .NET, we choose **.NET 5.0** as the framework and **C#** as the language.


## Usage
**Run Project :**

1.  clone/download the code
2.  open the subfolder  `MyProject.Api`  in VS Code. It should ask to add resources; press  `yes`.
3.  open a terminal
4.  `dotnet ef database update`  (this will create a .sqlite file)
5.  `dotnet watch run`
6.  [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html).

**Run tests :**
1.  clone/download the code
2.  open a terminal
3.  `dotnet test`  in the root  folder
4.  check the testrunner in the terminal.


**Not in Project :**

 1. On overview page show only first 1000 characters event description 
 2. On overview page show event paticipation count
 3. More tests required
 
