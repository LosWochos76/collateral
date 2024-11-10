#!/bin/sh

certbot certonly -m alexander.stuckenholz@gmail.com --agree-tos --non-interactive --authenticator dns-hetzner --dns-hetzner-credentials /root/certbot/hetzner.ini -d '*.stubwood.com'