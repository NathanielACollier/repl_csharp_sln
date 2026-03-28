# .devcontainer/Dockerfile

FROM mcr.microsoft.com/dotnet/sdk:8.0

# Install dependencies
RUN apt-get update && apt-get install -y \
    curl \
    git \
    ca-certificates \
    bash \
    && rm -rf /var/lib/apt/lists/*

# Install Ollama client (not the server)
RUN curl -fsSL https://ollama.com/install.sh | sh

# Set your remote Ollama endpoint
ENV OLLAMA_HOST=http://nathanieldesktop2:11434

# Create workspace directory
WORKDIR /workspace
