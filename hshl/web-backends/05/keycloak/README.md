# Keycloak und Mailpit für Kapitel 05

Dieser Ordner startet zwei Dienste als Docker-Container:

- **Keycloak** (Identity and Access Management) mit dem Realm `todo` aus `realm-todo.json`,
- **Mailpit** (Test-Mailserver), der alle Mails abfängt, z. B. die Mails zum Zurücksetzen des Passworts.

Was Docker und Docker Compose genau tun, behandelt Kapitel 11. Hier genügen die folgenden Befehle.

## Starten

```console
cd keycloak
docker compose up -d
```

Keycloak braucht beim ersten Start etwa 10 bis 20 Sekunden. Bereit ist es, sobald
<http://localhost:8080/realms/todo/.well-known/openid-configuration> antwortet.

## Adressen und Zugangsdaten

| Dienst | Adresse | Zugang |
|---|---|---|
| Keycloak-Admin-Konsole | <http://localhost:8080/admin> | `admin` / `admin` |
| Realm `todo` | <http://localhost:8080/realms/todo> | |
| Mailpit (Weboberfläche) | <http://localhost:8025> | |
| Mailpit (SMTP) | `localhost:1025` | ohne Anmeldung |

## Inhalt des Realms `todo`

| Benutzer | Passwort | Rollen |
|---|---|---|
| `alice` | `alice` | `User` |
| `bob` | `bob` | `User`, `Admin` |

| Client | Art | Zweck |
|---|---|---|
| `todo-ui` | vertraulich, Secret `todo-ui-secret` | Web-UI, Authorization Code Flow mit PKCE |
| `todo-api` | nur Audience | REST-API, prüft die Access Tokens |
| `todo-cli` | vertraulich, Secret `todo-cli-secret` | Client Credentials (Maschine zu Maschine), Rolle `User` |
| `postman` | öffentlich | Authorization Code Flow mit PKCE aus Postman |

Ein Mapper schreibt die Realm-Rollen als Liste `roles` in ID Token, Access Token und UserInfo,
ein zweiter Mapper trägt `todo-api` als Audience ins Access Token ein. Der Password Grant
(Direct Access Grants) ist für alle Clients ausgeschaltet.

Die Secrets und Passwörter sind **nur für die lokale Entwicklung** gedacht. Im Betrieb kommen sie
aus Umgebungsvariablen oder `dotnet user-secrets` und werden nie ins Repository eingecheckt.

## Stoppen und Zurücksetzen

```console
docker compose down       # Container stoppen und entfernen
docker compose down -v    # zusätzlich alle Daten verwerfen
```

Beim nächsten `docker compose up -d` wird der Realm `todo` neu importiert, falls er noch nicht existiert.
`start-dev` ist nur für die Entwicklung gedacht (kein HTTPS, eingebaute Datenbank).
