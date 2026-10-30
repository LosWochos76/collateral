# Kapitel 05: Authentication and Authorization

Beispielcode zu Kapitel 05: die ToDo-REST-API (`api`), die ToDo-Web-UI (`ui`) und gemeinsamer Code (`common`).
Ein Konfigurationswert schaltet zwischen zwei Modellen um:

- `Authentication:Mode` = `Local` (Vorgabe): eigene Benutzer, JWT in der API, Cookies in der UI, optional Google und Facebook,
- `Authentication:Mode` = `Keycloak`: Benutzer, Rollen und Anmeldung liegen in Keycloak.

## 1. Voraussetzungen

- .NET 10 SDK (`dotnet --list-sdks`)
- Entwicklerzertifikat für HTTPS: `dotnet dev-certs https --trust`
- Docker, nur für Keycloak und Mailpit (siehe `keycloak/README.md`)

Die Seed-Benutzer im Modus `Local`:

| E-Mail | Passwort | Admin |
|---|---|---|
| `alexander.stuckenholz@hshl.de` | `secret` | ja |
| `student@hshl.de` | `secret` | nein |

## 2. API im Modus `Local`

```console
dotnet run --project api --launch-profile https
```

Token holen und verwenden:

```console
curl -k -H "Content-Type: application/json" \
     -d '{"EMail":"alexander.stuckenholz@hshl.de","Password":"secret"}' \
     https://localhost:7088/Login

curl -k -H "Authorization: Bearer <access_token>" https://localhost:7088/ToDo/
```

Falsche Zugangsdaten ergeben `401`, `DELETE /ToDo/{id}` mit dem Token von `student@hshl.de` ergibt `403`.
Der JWT-Schlüssel für die Entwicklung steht in `api/appsettings.Development.json`; im Betrieb kommt er aus
einer Umgebungsvariablen (`Jwt__Key`) oder aus `dotnet user-secrets`.

## 3. UI im Modus `Local`

```console
cd keycloak && docker compose up -d && cd ..   # Mailpit für die Reset-Mails
dotnet run --project ui --launch-profile https
```

- <https://localhost:7031> öffnen und mit einem Seed-Benutzer anmelden.
- "Password forgotten" ausprobieren: Die Mail erscheint in Mailpit unter <http://localhost:8025>.
  Der Link ist 30 Minuten und nur einmal gültig.
- Löschen darf nur der Admin; `student@hshl.de` landet auf der Seite "Access Denied".

## 4. Google und Facebook

1. Die Anwendung beim Anbieter registrieren (Kurzfassung auf den Folien):
   - Google: Google Cloud Console, "Google Auth Platform", OAuth-Client vom Typ "Web application",
     Redirect URI `https://localhost:7031/signin-google`.
   - Facebook: Meta for Developers, App mit dem Anwendungsfall "Facebook Login",
     Redirect URI `https://localhost:7031/signin-facebook`.
2. Die Zugangsdaten **nur** als User Secrets hinterlegen, nie in einer Datei im Repository:

```console
cd ui
dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>"
dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>"
dotnet user-secrets set "Authentication:Facebook:AppId" "<app-id>"
dotnet user-secrets set "Authentication:Facebook:AppSecret" "<app-secret>"
```

Nur für Anbieter mit Zugangsdaten erscheint auf der Login-Seite ein Button.

## 5. Keycloak

```console
cd keycloak && docker compose up -d && cd ..
dotnet run --project ui --launch-profile https -- --Authentication:Mode=Keycloak
dotnet run --project api --launch-profile https -- --Authentication:Mode=Keycloak
```

Statt des Kommandozeilenparameters geht auch die Umgebungsvariable `Authentication__Mode=Keycloak`.

- UI: <https://localhost:7031> leitet zur Anmeldung bei Keycloak um. `alice`/`alice` darf nicht löschen,
  `bob`/`bob` ist Admin.
- API: ein Token per Client Credentials holen und verwenden:

```console
curl -d grant_type=client_credentials -d client_id=todo-cli \
     -d client_secret=todo-cli-secret \
     http://localhost:8080/realms/todo/protocol/openid-connect/token

curl -k -H "Authorization: Bearer <access_token>" https://localhost:7088/ToDo/
```

Im Modus `Keycloak` gibt es den Endpunkt `/Login` der API nicht mehr (`404`); die Tokens stellt Keycloak aus.
Das Client-Secret `todo-ui-secret` steht nur für die Entwicklung in `ui/appsettings.Development.json`.
