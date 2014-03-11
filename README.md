Problem/Solution
================

If you're using [Aron Granberg's A* package](http://arongranberg.com/astar/) and [Toolkit 2D's](http://www.unikronsoftware.com/2dtoolkit/) tilemap solution with Unity's new 2D features, you might have a bit of a problem with A* finding your tiles. This is because tk2d's tilemap generates colliders using `EdgeCollider2D` meaning your "walls" are not solid. This simple script aims to fix that problem. Basically it loops through your map, finds colliders and drops a `BoxCollider2D` into a colliders place.

Usage
=====

Drop the package into your project, create a empty gameObject, add the ColliderBuilder.cs to it. Tell ColliderBuilder what map you're using and what layer you want the colliders to be draw on (this is for A*'s `collision testing` feature), then hit "Create Colliders". Finally head over to A*, set the `collisions testings` mask to your chosen layer and scan the grid. If everything goes well and your walls are found, simply cache the grid then click "Remove Colliders" to remove the generated `BoxCollider2D's`.

Limitations
===========

This scripts finds tiles with ANY collider on it and draws a collider in that position. That means it doesn't care if it's a box collider, polygon collider or circle collider.