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
3.  `dotnet test`  in the  `api.test`  folder
4.  check the testrunner in the terminal.


## Todo

**Project :**

 1. On overview page show event description (first 1000 characters) → **detail to fix later**
 2. The user gets a button to enroll for the event when he/she is not enrolled yet. After he/she is enrolled, the button changes to a cancel button. → **Need to figure out how to use foreign keys**
 3.  On overview page show event paticipation count → **Need working foreign keys for this**
 4. When the age range is not filled in, it's an all-ages event and everyone sees the event in their list. → **detail to fix later**
 
**Question:** An administrator can remove a person from an event = After he/she is enrolled, the button changes to a cancel button? (same route?)

**Tests:**

 1. write own tests for our api routes → **don't know how to do this**
