# ContentCanvas
Goal: To learn a fullstack with React TypeScript for the frontend and ASP.NET Core for the backend.

## Overview

ContentCanvas is a full-stack web application designed as a creative space for digital expression. It's an ideal platform for blogging or HTML content posting, allowing users to articulate and share their ideas like an artist on a canvas. Built with React TypeScript for the frontend and ASP.NET Core for the backend, it integrates user authentication, role-based access control, and uses a NoSQL database for efficient data management.

## Features

- User Authentication (Register/Login)
- Role-Based Access Control (User, Moderator, Admin)
- Admins can confirm user accounts and assign roles
- Users can create, edit, and view public and private blog posts
- Moderators and admins have enhanced content management capabilities

## Tech Stack

- **Frontend**: React TypeScript
- **Backend**: ASP.NET Core Web API
- **Database**: NoSQL (e.g., MongoDB)

## Getting Started

### Prerequisites

- Visual Studio 2019 or later
- Node.js and npm
- MongoDB or a preferred NoSQL database

### Setting Up the Project

1. **Clone the Repository**

   ```
   git clone https://github.com/AliSafari-IT/ContentCanvas.git
   ```

2. **Set Up the Backend**
   - Open the backend project in Visual Studio.
   - Restore NuGet packages.
   - Update the database connection string in `appsettings.json`.
   - Run the project to start the backend server.

3. **Set Up the Frontend**
   - Navigate to the frontend directory.
   - Run `npm install` to install dependencies.
   - Run `npm start` to start the React app.

## Usage

- Access the web application through the URL provided by the React development server.
- Register as a new user and log in to explore.
- Utilize features based on your user role.

## Contributing

We welcome contributions to ContentCanvas. Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch for your feature.
3. Commit your changes.
4. Push to the branch.
5. Create a new Pull Request.

## License

This project is licensed under the [MIT License](LICENSE.md).

## Recent Updates (Branch: AddRedux)

- Integrated Redux in the React TypeScript frontend for efficient state management.
- Added Redux Thunk for asynchronous actions, primarily for fetching and managing user data.
- Implemented a comprehensive user management system including fetching, sorting, and displaying user information and roles.
- Enhanced role management functionality, allowing for more dynamic control over user permissions.
- Refined data fetching methods to ensure seamless synchronization with the ASP.NET Core backend.
- Improved error handling and data loading states in the user interface for better user experience.
- Conducted extensive testing and debugging to ensure robust performance and reliability.
