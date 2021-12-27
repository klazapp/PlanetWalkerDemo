# DESCRIPTION

PlanetWalkerDemo is a simple game that demonstrates how a planet walking logic may be implemented. A similar logic was implemented in the game Planet Om Nom Nom that is available on both [GooglePlay](https://play.google.com/store/apps/details?id=com.klausology.planetomnomnom) and [Apple](https://apps.apple.com/us/app/planet-om-nom-nom/id1521638287) app store.

## Documentation

The basic planet walker logic uses a parent-child combination to execute a spherical movement around the planet. The parent is centered to the planet and provides the basic planet movement, the child is used as an indicator to the direction the player might be facing.

## Showcase

![](https://github.com/klazapp/PlanetWalkerDemo/blob/main/Assets/GifShowCase/Showcase-1.gif)


This gif shows basic player movement, bullet movement and enemy movement.

## Limitations

The goal of this project is to demonstrate a version wherein a planet walker logic may be implemented. Since the logic revolves around being centered to the planet, a non spherical planet will not work well. The bullets interacting with the enemies are also not implemented.

## TODO

- Simplify gameobject hierarchy by removing the need to have a parent-child relationship
- Explore alternative planet walking logics