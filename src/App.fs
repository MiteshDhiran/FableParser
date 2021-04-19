module App
open Fable.Core.JsInterop
open Fable.Import
open System
open CustomParser

let digitListResult = run (sepBy1 pInt comma) "12,34"

let resutString = match digitListResult with
                  | Success (list, rest)
                      -> list |> List.fold (fun s item -> s + item) 0
                  | Failure err ->
                      0
                      
let window = Browser.Dom.window

// Get our canvas context 
// As we'll see later, myCanvas is mutable hence the use of the mutable keyword
// the unbox keyword allows to make an unsafe cast. Here we assume that getElementById will return an HTMLCanvasElement 
let mutable myCanvas : Browser.Types.HTMLCanvasElement = unbox window.document.getElementById "myCanvas"  // myCanvas is defined in public/index.html

// Get the context
let ctx = myCanvas.getContext_2d()

// All these are immutables values
let w = myCanvas.width
let h = myCanvas.height
let steps = 20
let squareSize = 20

// gridWidth needs a float wo we cast tour int operation to a float using the float keyword
let gridWidth = float (steps * squareSize) 

// resize our canvas to the size of our grid
// the arrow <- indicates we're mutating a value. It's a special operator in F#.
myCanvas.width <- gridWidth
myCanvas.height <- gridWidth

// print the grid size to our debugger consoloe
printfn "%i" steps

// prepare our canvas operations
[0..steps] // this is a list
  |> Seq.iter( fun x -> // we iter through the list using an anonymous function
      let v = float ((x) * squareSize) 
      ctx.moveTo(v, 0.)
      ctx.lineTo(v, gridWidth)
      ctx.moveTo(0., v)
      ctx.lineTo(gridWidth, v)
    ) 
ctx.strokeStyle <- !^"#ddd" // color

// draw our grid
ctx.stroke() 

// write Fable
ctx.textAlign <- "center"
ctx.fillText(resutString.ToString(), gridWidth * 0.5, gridWidth * 0.5)

printfn "done!"


