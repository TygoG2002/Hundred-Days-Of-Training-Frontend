# Hundred Days Of Training – Frontend

This repository contains the **frontend application** for the *Hundred Days Of Training* system.

The frontend is a **Blazor WebAssembly (WASM) Progressive Web App (PWA)** that runs entirely in the browser.  
It is designed to track daily workouts, manage training schedules, and visualize long-term progress — with a strong focus on **calisthenics and weighted strength training**.

I built this project for **personal use** and as a **portfolio project**, with the goal of creating a fast, app-like experience for following a structured **100-day training program**.

---

## Role within the system

The frontend is one component of a **private end-to-end training system**:

- Runs as a **Blazor WebAssembly (WASM)** application in the browser
- Communicates with a backend API over HTTP
- Both frontend and backend are hosted on a **Raspberry Pi Linux server**
- Access is restricted to a **private Tailscale network**

All business logic, validation, and data persistence are handled by the backend API.  
The frontend focuses on **user interaction**, **state management**, and **presentation**.

---

## System architecture

The frontend is served as static files and communicates with the backend through a private network.

- Client devices connect via **Tailscale**
- **Nginx** serves the WASM files and static assets
- API requests are proxied to the ASP.NET Core backend
- All communication stays inside the private network

Because the application is not publicly accessible, screenshots are included below to demonstrate the UI and functionality.

<table>
  <tr>
    <td align="center">
      <img
        src="https://github.com/user-attachments/assets/b0e07a44-603a-47ac-b9c2-f5cb6d4f3a42"
        alt="System architecture diagram"
        width="700"
      />
    </td>
  </tr>
</table>

---

## Performance considerations

Because the app runs fully as **WebAssembly in the browser**, performance was an important design focus:

- Client-side caching to reduce API calls
- Optimistic UI updates during workouts
- Minimal re-renders for smooth interaction
- App-like navigation using PWA capabilities

This keeps the application responsive, even on mobile devices and when hosted on low-power hardware such as a Raspberry Pi.

---

## Screenshots

The application runs inside a private network and cannot be accessed publicly.  
The screenshots below show the core functionality and user experience on **iPhone**.

---

### Dashboard

The dashboard provides an overview of **which workouts need to be done today**.

It also allows you to **plan workouts for the rest of the week** by dragging and dropping workout cards.  
This makes it easy to adjust your schedule without changing the underlying training programs.

<table>
  <tr>
    <td align="center">
      <img
        src="https://github.com/user-attachments/assets/378fdb6d-d8bb-45c4-ac93-f32537528c4d"
        alt="Dashboard"
        width="300"
      />
    </td>
  </tr>
</table>

---

### Workout Templates

This screen shows all **workout templates** that have been created.

Each template represents a complete workout.  
By tapping on a card, a workout session can be started immediately.

<table>
  <tr>
    <td align="center">
      <img
        src="https://github.com/user-attachments/assets/3a123e06-10cb-4f84-80f0-a5e180ad2dbf"
        alt="Workout templates"
        width="300"
      />
    </td>
  </tr>
</table>

---

### 100 Day Programs

This view displays the structured **100-day training programs**.

Progress is tracked per day, helping to enforce long-term consistency and discipline.

<table>
  <tr>
    <td align="center">
      <img
        src="https://github.com/user-attachments/assets/9a4493b6-4ab7-4563-ba9d-bb148cfda486"
        alt="100 day programs"
        width="300"
      />
    </td>
  </tr>
</table>

---

### Calendar

The calendar provides a **visual overview of completed and planned workouts**.

It helps identify training patterns, rest days, and overall consistency over time.

<table>
  <tr>
    <td align="center">
      <img
        src="https://github.com/user-attachments/assets/b4e4d2b6-d0d6-478a-8e44-587386a2a0eb"
        alt="Calendar view"
        width="300"
      />
    </td>
  </tr>
</table>

---

## Security & access

- The frontend is **not publicly accessible**
- It is served only inside a **private Tailscale network**
- No authentication is implemented by design
- Security is handled at the **network level**

This setup is intended for **personal and internal use**.

---

## Tech stack

- **Blazor WebAssembly (WASM)**
- **.NET / C#**
- **HTML / CSS**
- **Progressive Web App (PWA)**
- **Client-side caching**
- **Nginx (static file hosting)**
- **Linux (Raspberry Pi)**
- **Tailscale (private networking)**

---

## Notes

This repository contains **only the frontend application**.  
The backend API and infrastructure configuration are maintained in separate repositories.
