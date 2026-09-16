FROM python:3.12-alpine

WORKDIR /app
COPY modbus_meter /app/modbus_meter

RUN addgroup -S app && adduser -S -G app app
USER app

EXPOSE 1502
CMD ["python3", "-m", "modbus_meter.server", "--host", "0.0.0.0", "--profile", "pv"]
