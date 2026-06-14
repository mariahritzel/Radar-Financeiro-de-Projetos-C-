import { useEffect, useState } from "react";
import axios from "axios";
import "./../styles/dashboard.css";

export default function Dashboard() {
  const [dados, setDados] = useState(null);
  const [projetos, setProjetos] = useState([]);
  const [projetoSelecionado, setProjetoSelecionado] = useState(null);

useEffect(() => {
  async function carregarDados() {
    try {
      const token = localStorage.getItem("token");

      // URL dinâmica baseada no filtro
      const dashboardUrl = projetoSelecionado
        ? `http://localhost:5279/api/Projetos/${projetoSelecionado}/dashboard`
        : `http://localhost:5279/api/Dashboard/pesquisador/1`;

      const dashboardResponse = await axios.get(dashboardUrl, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });

      setDados(dashboardResponse.data);

      const projetosResponse = await axios.get(
        "http://localhost:5279/api/Pesquisadores/1/projetos",
        {
          headers: {
            Authorization: `Bearer ${token}`
          }
        }
      );

      setProjetos(projetosResponse.data);

    } catch (err) {
      console.log("Erro ao carregar dados", err);
    }
  }

  carregarDados();

}, [projetoSelecionado]);

  if (!dados) return <p>Carregando...</p>;

  return (
    <div className="dashboard">
      <h1>Dashboard</h1>
      
       <select
          onChange={(e) => setProjetoSelecionado(e.target.value)}
          value={projetoSelecionado || ""}
        >
          <option value="">Todos os projetos</option>

          {projetos.map((p) => (
            <option key={p.id} value={p.id}>
              {p.nome}
            </option>
          ))}
        </select>

      <div className="cards">

        <div className="card">
          <h3>Projetos</h3>
          <p>{projetos.length}</p>
        </div>

        <div className="card">
          <h3>Receita total</h3>
          <p>{dados.totalReceitas}</p>
        </div>

        <div className="card">
          <h3>Despesas realizadas</h3>
          <p>{dados.totalDespesas}</p>
        </div>

        <div className="card">
          <h3>Disponível</h3>
          <p>{dados.valorDisponivel}</p>
        </div>

      </div>
    </div>
  );
}
