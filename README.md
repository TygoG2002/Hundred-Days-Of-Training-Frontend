# Hundred Days Of Training Frontend

This repository contains the **frontend application** for the *Hundred Days Of Training* workout system.

The frontend is a **Blazor WebAssembly (WASM) Progressive Web App (PWA)** that runs entirely in the browser and is used to track daily workouts and visualize long-term progression — with a focus on **weighted pull-ups and push-ups**.

I built this project both for **personal use** and as a **portfolio project**, because I wanted a fast, app-like interface to follow my training progress over a structured 100-day program.

---

## Role within the system

The frontend is one component of a **private end-to-end system**:

- The frontend runs as a **Blazor WebAssembly (WASM)** app in the browser
- It communicates with a backend API over HTTP
- Both are hosted on a **Raspberry Pi Linux server**
- Access is restricted to a **private Tailscale network**

All business logic and data validation are handled by the backend API.  
The frontend focuses on user interaction, state management, and presentation.

---

## System architecture

The frontend is served as static files and communicates with the backend through a private network.

- Client devices connect via **Tailscale**
- **Nginx** serves the WASM files and static assets
- API requests are proxied to the ASP.NET Core backend
- All communication stays within the private network

Because the application is not publicly accessible, screenshots are included below to demonstrate the UI and functionality.

<img width="1536" height="1024" alt="System architecture diagram" src="https://github.com/user-attachments/assets/b0e07a44-603a-47ac-b9c2-f5cb6d4f3a42" />

---

## Performance considerations

Because the app runs fully as **WebAssembly in the browser**, performance is an important focus:

- Client-side caching to reduce API calls
- Optimistic UI updates during workouts
- Minimal re-renders for smooth interaction

This keeps the app responsive even on mobile devices and when hosted on low-power hardware such as a Raspberry Pi.

---

## Screenshots

The application runs inside a private network and cannot be accessed publicly.  
The screenshots below show the core functionality and user experience.

### Dashboard

<img width="946" height="2048" alt="image" src="https://github.com/user-attachments/assets/378fdb6d-d8bb-45c4-ac93-f32537528c4d" />


### Workout Templates

<img width="946" height="2048" alt="image" src="https://github.com/user-attachments/assets/3a123e06-10cb-4f84-80f0-a5e180ad2dbf" />

### 100 Day Programs

<img width="946" height="2048" alt="image" src="https://github.com/user-attachments/assets/9a4493b6-4ab7-4563-ba9d-bb148cfda486" />

### Calendar

<img width="946" height="2048" alt="image" src="https://github.com/user-attachments/assets/b4e4d2b6-d0d6-478a-8e44-587386a2a0eb" />

---

## Security & Access

- The frontend is **not publicly accessible**
- It is served only inside a **private Tailscale network**
- No authentication is implemented by design
- Security is handled at the **network level**

This setup is suitable for personal and internal use.

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
