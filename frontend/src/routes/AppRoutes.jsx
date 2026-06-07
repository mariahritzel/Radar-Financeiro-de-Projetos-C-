import { BrowserRouter, Routes, Route } from "react-router-dom";

function Login() {
  return <h2>Login</h2>;
}

function Dashboard() {
  return <h2>Dashboard</h2>;
}

function Projetos() {
  return <h2>Projetos</h2>;
}

export default function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/projetos" element={<Projetos />} />
      </Routes>
    </BrowserRouter>
  );
}