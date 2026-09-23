FROM python:3.12-alpine

WORKDIR /app
COPY weather_station /app/weather_station

RUN addgroup -S app && adduser -S -G app app
USER app

EXPOSE 1502
CMD ["python3", "-m", "weather_station.server", "--host", "0.0.0.0"]
