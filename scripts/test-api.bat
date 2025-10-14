@echo off
REM Script de teste para Energia API (Windows)
echo 🧪 Testando Energia API...

REM Aguardar a aplicação inicializar
echo ⏳ Aguardando aplicação inicializar...
timeout /t 10 /nobreak >nul

REM Testes básicos
echo 🔍 Executando testes...

REM Teste 1: Health Check
echo Testando Health Check...
curl -s -o nul -w "%%{http_code}" http://localhost:5028/health
if %errorlevel% equ 0 (
    echo ✅ Health Check OK
) else (
    echo ❌ Health Check FALHOU
)

REM Teste 2: Swagger UI
echo Testando Swagger UI...
curl -s -o nul -w "%%{http_code}" http://localhost:5028/swagger
if %errorlevel% equ 0 (
    echo ✅ Swagger UI OK
) else (
    echo ❌ Swagger UI FALHOU
)

echo.
echo 📊 Status dos containers:
docker-compose ps

echo.
echo 📝 Logs recentes da API:
docker-compose logs --tail=10 api

echo.
echo ✅ Testes concluídos!
echo 🌐 Acesse: http://localhost:5028/swagger
pause
