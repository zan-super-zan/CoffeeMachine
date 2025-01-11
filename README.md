# Coffee Machine Application

A web application for managing a coffee machine, allowing users to power it on/off, brew coffee, add resources (water and coffee), and view the machine's event history. This project showcases my skills in ASP.NET MVC, Docker, PostgreSQL, and frontend design.

---

## Features

- **Manage Coffee Machine:**
  - Turn the coffee machine on/off.
  - Add coffee and water to the machine.
  - Clean the machine.
- **Brew Coffee:**
  - Single, double, single long, and double long coffee types supported.
- **Event History:**
  - View a detailed log of all events (e.g., brewing, adding resources).
- **Modern UI:**
  - Styled using CSS with responsive design and interactive elements.
- **Database Integration:**
  - PostgreSQL database for persisting machine state and event logs.
- **Containerized Application:**
  - Dockerized API, MVC frontend, and database for easy deployment.

---

## Technologies Used

### Backend
- **ASP.NET Core API**: RESTful API for managing coffee machine operations.
- **Entity Framework Core**: For database interaction with PostgreSQL.
- **C#**: Primary programming language.

### Frontend
- **ASP.NET MVC**: For building a responsive and user-friendly UI.
- **CSS**: For modern styling with hover effects and responsive design.

### Database
- **PostgreSQL**: Stores the coffee machine state and event history.

### Containerization
- **Docker**: Containers for API, frontend, and database orchestrated with Docker Compose.

---

## Project Setup

### Prerequisites

Ensure you have the following installed on your machine:
- [Docker](https://www.docker.com/)
- [.NET SDK](https://dotnet.microsoft.com/download)
- PostgreSQL client (optional, for direct database access)

### Clone the Repository

```bash
git clone https://github.com/your-repo/coffee-machine.git
cd coffee-machine
