Problem/Solution
================

If you're using [Aron Granberg's A* package](http://arongranberg.com/astar/) and [Toolkit 2D's](http://www.unikronsoftware.com/2dtoolkit/) tilemap solution with Unity's new 2D features, you might have a bit of a problem with A* finding your tiles. This is because tk2d's tilemap generates colliders using EdgeCollider2D and your "walls" are not solid. This simple script aims to fix that problem, or provides a building block for you to implement something of your own. Basically it loops through your map, finds colliders and drops a BoxCollider2D into a colliders place.


Prerequirements
===============

This script only requires you to have a Layer named `Pathfinding Grid`. You'll use this Layer inside the A* package to find your walls as well.


Usage
=====

Drop the package into your project, create a empty gameObject, add the CreateBoxCollider.cs, drop your tilemap into the empty gameObject's inspect, then finally hit "Create Colliders". Then head over to A* and scan the grid. If everything goes well and your walls are found, simply cache the grid then click "Remove Colliders" to remove the generated BoxCollider2D's.