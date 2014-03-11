Problem/Solution
================

If you're using [Aron Granberg's A* package](http://arongranberg.com/astar/) and [Toolkit 2D's](http://www.unikronsoftware.com/2dtoolkit/) tilemap solution with Unity's new 2D features, you might have a bit of a problem with A* finding your tiles. This is because tk2d's tilemap generates colliders using EdgeCollider2D meaning your "walls" are not solid. This simple script aims to fix that problem. Basically it loops through your map, finds colliders and drops a BoxCollider2D into a colliders place.

Usage
=====

Drop the `ColliderBuilder` folder into your project, then create an empty GameObject add the ColliderBuilder.cs script to it. Tell ColliderBuilder what map you're using and what layer you want the colliders to be draw on (this is for A*'s collision testing feature), then hit `Create Colliders`. Finally head over to A*s graph generator, set the collisions testings mask to your chosen layer and scan the grid. If everything goes well and your walls are found, simply cache the grid then click "Remove Colliders" to remove the generated BoxCollider2D's.

Limitations
===========

This scripts finds tiles with ANY collider on it and draws a collider in that position. That means it doesn't care if it's a box collider, polygon collider or circle collider.

Visual Guide
============

1. Set Layer ![Step 1](https://raw.github.com/kevdotbadger/BoxColliderGenerator/master/img/choose-tilemap-layer.png)

2. Generate Colliders ![Step 2](https://raw.github.com/kevdotbadger/BoxColliderGenerator/master/img/preview-of-colliders.png)

3. Set Layer (Mask) in A*  ![Step 3](https://raw.github.com/kevdotbadger/BoxColliderGenerator/master/img/choose-mask-in-astar.png)

4. Generate A* graph  ![Step 4](https://raw.github.com/kevdotbadger/BoxColliderGenerator/master/img/generated-map.png)