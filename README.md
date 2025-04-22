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
