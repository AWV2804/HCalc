//
//  ContentView.swift
//  HCalc
//
//  Created by Atharva Rao on 6/19/24.
//

import SwiftUI

struct ContentView: View {
    @State private var input = ""
    @State private var decimalOutput = ""
    @State private var hexOutput = ""
    @State private var binaryOutput = ""
    
    var body: some View {
        VStack {
            TextField("Enter a number", text: $input)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(.top)
                .padding(.horizontal)
                .onChange(of: input) { newValue in updateOutputs(input: newValue)
                }
            
            HStack {
                Text("Decimal: \(decimalOutput)")
                Text("Hexadecimal: \(hexOutput)")
                Text("Binary: \(binaryOutput)")
                Button("Bit Visualizer") {
                    openBitsVisualizer()
                }
            }
            .padding()
            
            Spacer()
        }
        .frame(width: 400, height: 200)
        .padding()
    }
    
    func updateOutputs(input: String) {
        if input.hasPrefix("0x") {
            // Hexadecimal input
            let hexString = String(input.dropFirst(2))
            if let decimalValue = Int(hexString, radix: 16) {
                decimalOutput = String(decimalValue)
                hexOutput = "0x\(hexString.uppercased())"
                binaryOutput = String(decimalValue, radix: 2)
            } else {
                clearOutputs()
            }
        } else if input.contains(#/\d+(?=b)/#) {
            let binString = String(input.dropFirst(2))
            if let decimalValue = Int(binString, radix: 2) {
                decimalOutput = String(decimalValue)
                hexOutput = "0x\(String(decimalValue, radix: 16).uppercased())"
                binaryOutput = String(decimalValue, radix: 2)
            } else {
                clearOutputs()
            }
        }
        else {
            // Decimal input
            if let decimalValue = Int(input) {
                decimalOutput = String(decimalValue)
                hexOutput = "0x\(String(decimalValue, radix: 16).uppercased())"
                binaryOutput = String(decimalValue, radix: 2)
            } else {
                clearOutputs()
            }
        }
    }
    
    func clearOutputs() {
        decimalOutput = "Invalid Input"
        hexOutput = "Invalid Input"
        binaryOutput = "Invalid Input"
    }
    
    func openBitsVisualizer() {
        let bitsVisualizer = BitsVisualizer()
        let hostingController = NSHostingController(rootView: bitsVisualizer)
        
        let window = NSWindow(contentViewController: hostingController)
        window.setContentSize(NSSize(width: 400, height: 200))
        window.styleMask = [.titled, .closable, .resizable]
        window.title = "Bits Visualizer"
        window.makeKeyAndOrderFront(nil)
    }
}
    #Preview {
        ContentView()
    
}
