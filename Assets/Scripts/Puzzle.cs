/* Ben Scott * bescott@andrew.cmu.edu * 2015-11-18 * Puzzle */

using UnityEngine;
using System.Collections.Generic;
using EventArgs=System.EventArgs;

/** `OnSolve<T>` : **`event`**
 *
 * Allows for inversion of control, from the lowest piece
 * to the most complex puzzle. When an `IPiece` is solved,
 * the parent should be notified via this `event`.
 *
 * - `sender` : **`T`**
 *     the `IPiece<T>` sending this event
 *
 * - `e` : **`EventArgs`**
 *     typical `event` arguments
 *
 * - `solved` : **`bool`**
 *     was the `sender` solved?
 **/
public delegate T OnSolve<T>(
    IPiece<T> sender,
    EventArgs e,
    bool solved);


/** `IPiece<T>` : **`interface`**
 *
 * An `IPiece` is an element of a larger `Puzzle`, and can
 * change the state of said `Puzzle` on the basis of its
 * own configuration. It could be solved, unsolved, or in
 * the case of more complicated base types, a piece could
 * represent a digit on a combination lock. In that case,
 * a given piece might not have its own solution, but could
 * represent a solved puzzle when considered in aggregate.
 **/
public interface IPiece<T> {


    /** `SolveEvent` : **`event`**
     *
     * Event callback for inversion of control. Inheritors
     * must at least notify subscribers in the event that
     * they are solved, and when they become unsolved.
     **/
    event OnSolve<T> SolveEvent;


    /** `IsSolved` : **`bool`**
     *
     * Whether or not the current state is the solution.
     * Inheritors which use value types as their generic
     * arguments should enforce the below contract. For any
     * inheritors which need to represent more complicated
     * states, implementation should maintain some parity
     * between `IsSolved` and their actual configuration.
     *
     * - `ensure` : `IsSolved==(Condition==Solution)`
     **/
    bool IsSolved {get;}


    /** `Condition` : **`T`**
     *
     * An instance's present configuration.
     **/
    T Condition {get;set;}


    /** `Solution` : **`T`**
     *
     * When the configuration of an instance is equal to
     * its `Solution`, it's considered solved.
     **/
    T Solution {get;set;}


    /** `Solve()` : **`T`**
     *
     * Generic approach to solving / resolving aspects of a
     * larger puzzle, or perhaps just one piece. The action
     * of solving might represent the pull of a lever, or
     * the placement of a piece in an actual jigsaw puzzle.
     *
     * - `condition` : **`T`**
     *   value to attempt to solve with
     **/
    bool Solve(T condition);
}










