# 🎙️ NewsPodcast

O **NewsPodcast** é um aplicativo WPF que resume notícias em uma linguagem natural e gera áudios para que você possa escutá-las com praticidade. Ideal para quem quer se manter informado enquanto realiza outras atividades.

---

## ✨ Funcionalidades

- 🧠 **Resumo de notícias** com linguagem fluida e objetiva.
- 🔊 **Conversão de texto para áudio** com vozes naturais usando a API do Google.
- 🔎 **Busca por temas ou categorias**, exibindo cards com as notícias mais relevantes.
- ▶️ **Controles de áudio**: play, pause, avançar/retroceder, barra de progresso.

---

## 🧱 Tecnologias Utilizadas

- **WPF (.NET)**
- **Google.Cloud.TextToSpeech**
- **API OpenAI** (para resumo de texto)
- **GNews API** (para obtenção de notícias)

---

## 🧭 Arquitetura

Mesmo sendo um projeto de pequeno porte, o **NewsPodcast** foi desenvolvido com uma arquitetura em camadas, buscando seguir os princípios da **Clean Architecture**. A proposta é manter o código desacoplado e escalável, permitindo futuras melhorias, substituição de tecnologias ou integração com novas fontes de dados e serviços.

---

🚀 Como Clonar e Rodar o Projeto
📋 Pré-requisitos
Antes de começar, você vai precisar das seguintes chaves e credenciais:

🔑 API Key da GNews (gratuita)

🔑 API Key da OpenAI (requer créditos — no meu caso, 5 dólares foram suficientes)

🔑 Credenciais da Google Cloud Text-to-Speech API (Google oferece créditos gratuitos por 3 meses)

⚠️ Essas chaves são essenciais para que o aplicativo funcione corretamente.
Os serviços da OpenAI e do Google Text-to-Speech exigem cadastro e podem ter custos, então é importante verificar os termos de uso.

Necessário adicionar as credenciais no appsettings.json do projeto WPF.
Após isso, clone o repositório na sua máquina e fique atualizado das noticias sobre os assuntos que te cercam.

🧪 Testes (opcional)
Caso deseje executar os testes automatizados, será necessário adicionar as mesmas credenciais no appsettings.json do projeto de testes.
