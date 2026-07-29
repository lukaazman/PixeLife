# PixeLife

2D pixel-art walking simulator built in Unity.

[▶ Play in browser](https://lukaazman.github.io/PixeLife/)

## Features

- Character resembling a Minion, walking through scenery inspired by Kranj
- Fast travel across the map by bus
- NPC dialogue with multiple choices, powered by Ink
- Simple menu, running and jumping
- Free, original soundtrack

## About

Built as a hands-on project to get familiar with the Unity engine - includes visits to some of my favorite spots in Kranj and a handful of interactive NPCs.

## Sprites

All artwork hand-drawn in Aseprite, with frame-by-frame animation.

## GitHub Pages build

Every push to `main` builds the Unity WebGL player and deploys it to GitHub Pages through GitHub Actions.

Repository setup required once:

1. Add the Unity personal license XML as the repository secret `UNITY_LICENSE`.
2. In **Settings -> Pages**, set **Source** to **GitHub Actions**.
