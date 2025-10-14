#!/bin/bash

# Script de setup para Energia API
echo "🚀 Configurando Energia API..."

# Verificar se Docker está instalado
if ! command -v docker &> /dev/null; then
    echo "❌ Docker não está instalado. Por favor, instale o Docker Desktop."
    exit 1
fi

# Verificar se Docker Compose está instalado
if ! command -v docker-compose &> /dev/null; then
    echo "❌ Docker Compose não está instalado. Por favor, instale o Docker Compose."
    exit 1
fi

# Criar arquivo .env se não existir
if [ ! -f .env ]; then
    echo "📝 Criando arquivo .env..."
    cp env.example .env
    echo "✅ Arquivo .env criado. Configure as variáveis conforme necessário."
else
    echo "✅ Arquivo .env já existe."
fi

# Criar diretório de logs
mkdir -p logs

# Criar diretório de scripts de inicialização do banco
mkdir -p init-scripts

echo "🔧 Configuração concluída!"
echo ""
echo "Para executar a aplicação:"
echo "  docker-compose up --build"
echo ""
echo "Para executar em background:"
echo "  docker-compose up --build -d"
echo ""
echo "Para parar a aplicação:"
echo "  docker-compose down"
