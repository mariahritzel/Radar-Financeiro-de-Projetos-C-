import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export default function Cadastro() {
    const navigate = useNavigate();

    const [form, setForm] = useState({
        nome: "",
        email: "",
        curso: "",
        departamento: "",
        senha: ""
    });

    const [erro, setErro] = useState("");
    const [sucesso, setSucesso] = useState("");

    const handleChange = (e) => {
        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            await axios.post(
                "http://localhost:5279/api/Pesquisadores",
                form
            );

            setSucesso("Cadastro realizado com sucesso!");

            setTimeout(() => {
                navigate("/");
            }, 1500);

        } catch (err) {
            setErro("Erro ao cadastrar usuário");
        }
    };

    return (
        <div style={{ maxWidth: 400, margin: "100px auto" }}>
            <h2>Cadastro</h2>

            <form onSubmit={handleSubmit}>

                <input
                    name="nome"
                    placeholder="Nome"
                    onChange={handleChange}
                    style={{ display: "block", marginBottom: 10, width: "100%" }}
                />

                <input
                    name="email"
                    placeholder="Email"
                    onChange={handleChange}
                    style={{ display: "block", marginBottom: 10, width: "100%" }}
                />

                <input
                    name="curso"
                    placeholder="Curso"
                    onChange={handleChange}
                    style={{ display: "block", marginBottom: 10, width: "100%" }}
                />

                <input
                    name="departamento"
                    placeholder="Departamento"
                    onChange={handleChange}
                    style={{ display: "block", marginBottom: 10, width: "100%" }}
                />

                <input
                    type="password"
                    name="senha"
                    placeholder="Senha"
                    onChange={handleChange}
                    style={{ display: "block", marginBottom: 10, width: "100%" }}
                />

                <button type="submit">
                    Cadastrar
                </button>

            </form>

            {erro && <p style={{ color: "red" }}>{erro}</p>}
            {sucesso && <p style={{ color: "green" }}>{sucesso}</p>}
            Já possui conta?{" "}
            <a href="/">
                Fazer login
            </a>
        </div>
    );
}