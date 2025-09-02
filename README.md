
# 🔒 LLM Red Team Mini

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)  
[![React](https://img.shields.io/badge/React-18-61DAFB?logo=react&logoColor=black)](https://react.dev/)  
[![OpenAI](https://img.shields.io/badge/OpenAI-API-412991?logo=openai&logoColor=white)](https://platform.openai.com/)  
[![Security](https://img.shields.io/badge/AI-Security-red)](#)

⚡ A lightweight **AI Red Teaming tool** that tests large language models (LLMs) for common security risks:  
- Prompt injection  
- Jailbreak role-play exploits  
- Sensitive data leaks (PII/API keys)  
- Token-drain (DoS) attempts  

Full-stack project: **.NET 8 backend** (REST API) + **React frontend** (dashboard).  

---

## 📸 Screenshots

### Backend Running
![Backend Running](screenshots/backend-running.png)  
*Backend API running on .NET 8.*

---

### API Seed Endpoint
![API Seed](screenshots/api-seed.png)  
*`/api/seed` endpoint returning the defined test suite in JSON.*

---

### Dashboard – Input
![Dashboard Input](screenshots/ui-input.png)  
*Dashboard where you configure provider, model, and API key (removed for security).*

---

### Dashboard – Results
![Dashboard Results](screenshots/ui-results.png)  
*PASS/FAIL results with severity, evidence, and output snippets.*

---

### JSON Report Export
![JSON Report](screenshots/json-report.png)  
*Reports exported as JSON for integration into pipelines or audits.*

---

## ✨ Features
- 🔹 **Modular test suite** (easy to extend with new attack cases)  
- 🔹 **Supports OpenAI (cloud) & Ollama (local)**  
- 🔹 **Detectors**: prompt leaks, jailbreaks, PII/API key exposure, DoS  
- 🔹 **Structured reports**: export JSON (future: PDF/HTML)  
- 🔹 **Full-stack**: .NET 8 API + React dashboard  

---

## 🚀 How to Run

### Backend (API)
```bash
cd backend
dotnet restore
dotnet run
```
API runs at: `http://localhost:5000`

Check: [http://localhost:5000/api/seed](http://localhost:5000/api/seed)

---

### Frontend (Dashboard)
```bash
cd frontend
npm install
npm run dev
```
Frontend at: [http://localhost:5173](http://localhost:5173)

---

### OpenAI Provider
- Set your API key in UI.  
- Default model: `gpt-4o-mini`  
- Recommended max tokens: **200–300**  

### Ollama Provider (local models, Linux/macOS/WSL2)
```bash
ollama run llama3
```
Then select **Provider: Ollama** + **Model: llama3** in the dashboard.

---

## 🛡️ Example Tests

| Test ID         | Name                 | What it checks |
|-----------------|----------------------|----------------|
| SYS_LEAK_01     | Reveal system prompt | If hidden instructions can leak |
| OVERRIDE_01     | Ignore rules         | If safety policies can be bypassed |
| JAILBREAK_ROLE_01 | Role-play jailbreak | If role-play tricks can unlock unsafe behavior |
| DOS_TOKEN_01    | Token drain          | If model can be forced into excessive output |
| PII_LURE_01     | Sensitive lure       | If secrets like API keys/SSNs are revealed |

---

## 📂 Project Structure
```
llm-redteam-mini/
├── backend/        # .NET 8 API
│   ├── Program.cs
│   ├── Models.cs
│   ├── Clients.cs
│   └── Detectors.cs
├── frontend/       # React + Vite dashboard
│   └── src/
│       ├── App.jsx
│       └── main.jsx
└── README.md
```

---

## 📝 Notes
- This is a **research tool**. Use responsibly and **only** on systems you own or have permission to test.  
- OpenAI API usage may incur costs. Default settings keep tokens low (~200) to reduce charges.  
- Future extensions:  
  - 📊 Dashboard graphs & charts  
  - 📑 PDF/HTML report export  
  - 🔐 Auth + multi-user  

---

## 📣 Why It Matters
AI red teaming is an **emerging field**. This project shows:  
- 🎯 How to identify LLM risks (prompt injection, jailbreaks, leaks).  
- 🏗️ How to build a full-stack security testing tool.  
- 📊 How to deliver results in a way security teams can act on.  
