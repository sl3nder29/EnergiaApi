#!/bin/bash

# Script de teste para Energia API
echo "🧪 Testando Energia API..."

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Função para testar endpoint
test_endpoint() {
    local url=$1
    local expected_status=$2
    local description=$3
    
    echo -n "Testando $description... "
    
    response=$(curl -s -o /dev/null -w "%{http_code}" "$url")
    
    if [ "$response" = "$expected_status" ]; then
        echo -e "${GREEN}✅ OK${NC}"
        return 0
    else
        echo -e "${RED}❌ FALHOU (Status: $response)${NC}"
        return 1
    fi
}

# Aguardar a aplicação inicializar
echo "⏳ Aguardando aplicação inicializar..."
sleep 10

# Testes básicos
echo "🔍 Executando testes..."

# Teste 1: Health Check
test_endpoint "http://localhost:5028/health" "200" "Health Check"

# Teste 2: Swagger UI
test_endpoint "http://localhost:5028/swagger" "200" "Swagger UI"

# Teste 3: API Root
test_endpoint "http://localhost:5028/api" "404" "API Root (404 esperado)"

echo ""
echo "📊 Status dos containers:"
docker-compose ps

echo ""
echo "📝 Logs recentes da API:"
docker-compose logs --tail=10 api

echo ""
echo "✅ Testes concluídos!"
echo "🌐 Acesse: http://localhost:5028/swagger"
