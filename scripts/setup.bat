@echo off
REM Script de setup para Energia API (Windows)

echo 🚀 Configurando Energia API...

REM Verificar se Docker está instalado
docker --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ Docker não está instalado. Por favor, instale o Docker Desktop.
    pause
    exit /b 1
)

REM Verificar se Docker Compose está instalado
docker-compose --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ Docker Compose não está instalado. Por favor, instale o Docker Compose.
    pause
    exit /b 1
)

REM Criar arquivo .env se não existir
if not exist .env (
    echo 📝 Criando arquivo .env...
    copy env.example .env
    echo ✅ Arquivo .env criado. Configure as variáveis conforme necessário.
) else (
    echo ✅ Arquivo .env já existe.
)

REM Criar diretório de logs
if not exist logs mkdir logs

REM Criar diretório de scripts de inicialização do banco
if not exist init-scripts mkdir init-scripts

echo 🔧 Configuração concluída!
echo.
echo Para executar a aplicação:
echo   docker-compose up --build
echo.
echo Para executar em background:
echo   docker-compose up --build -d
echo.
echo Para parar a aplicação:
echo   docker-compose down
pause
