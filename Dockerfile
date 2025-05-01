# Используем .NET 8.0 SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем csproj и восстанавливаем зависимости
COPY UserLoginForm.csproj ./
RUN dotnet restore

# Копируем остальные файлы и собираем проект
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Используем минимальный образ runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Открываем порт 80
EXPOSE 80

# Запуск приложения
ENTRYPOINT ["dotnet", "UserLoginForm.dll"]