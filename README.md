# Event Management Coding Challenge

This project is an application that allows users to create an “Event” system where they can create Guests and Events.

## Tech Stack

- Node.js 16.13.2
- Angular 14
- Bootstrap 4+
- .NET Core 6.0
- Entity Framework Core
- LocalDB

## Functionality

The application supports the following functionality:

### Guest Properties

- First Name (mandatory)
- Last Name (mandatory)
- Email (mandatory and unique)
- Allergies (not mandatory, can be many)

### Event Properties

- Event Name (mandatory, unique)
- Event Date (mandatory)
- List of Guests (mandatory, at least 2)
  - A guest can be invited to multiple events

## How to Run Locally

1. Clone the project: `gh repo clone MusmanShahzad/Event-management`

2. Install dependencies:

   - Go to the client folder: `cd client`
   - Run `npm install`

3. Start the project:
   - Open `EventManagement.sln` in Visual Studio
   - Go to Tools > NuGet Package Manager > Package Manager Console
   - Run the command `Add-Migration <migration_name>`
   - Run the command `Update-Database`
   - Change the database connection path in `appsettings.json`

## Demo

To see a demo of the application, visit this [link](https://www.loom.com/share/53e4eb273d704abeb5351b92255cccdd).

## Optimizations

To optimize the application, we can add client-side state management to reduce the number of API calls on every CRUD operation.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.
