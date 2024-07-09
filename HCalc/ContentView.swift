//
//  ContentView.swift
//  HCalc
//
//  Created by Atharva Rao on 6/19/24.
//

import SwiftUI

struct ContentView: View {
    @State private var numInput: String = ""
    @EnvironmentObject var Values: Values
    
    var body: some View 
    {
        VStack(alignment: .center, spacing: 10) 
        {
            TextField("Enter number", text: $numInput)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .frame(minWidth: 220)
                .fixedSize(horizontal: /*@START_MENU_TOKEN@*/true/*@END_MENU_TOKEN@*/, vertical: /*@START_MENU_TOKEN@*/true/*@END_MENU_TOKEN@*/)
                .padding(.all, 5)
                .onChange(of: numInput) 
                { _ in
                    self.numInputTextChanged(shiftedValues: false)
                }

            HStack(alignment: .center, spacing: 15) {
                HStack(alignment: .center, spacing: 15) {
                    Text("Decimal:")
                        .fontWeight(.bold)
                    TextField("", text: $Values.decValue)
                        .textFieldStyle(RoundedBorderTextFieldStyle())
                        .frame(minWidth: 60)
                        .fixedSize(horizontal: true, vertical: /*@START_MENU_TOKEN@*/true/*@END_MENU_TOKEN@*/)
                        .disabled(true)
                }
//                .frame(minWidth: 30)
//                .fixedSize(horizontal: /*@START_MENU_TOKEN@*/true/*@END_MENU_TOKEN@*/, vertical: /*@START_MENU_TOKEN@*/true/*@END_MENU_TOKEN@*/)
                HStack(alignment: .center, spacing: 5) {
                    Text("Hex:")
                        .fontWeight(.bold)
                    TextField("", text: $Values.hexValue)
                        .textFieldStyle(RoundedBorderTextFieldStyle())
                        .frame(minWidth: 60)
                        .fixedSize(horizontal: true, vertical: /*@START_MENU_TOKEN@*/true/*@END_MENU_TOKEN@*/)
                        .disabled(true)
                }
                HStack(alignment: .center, spacing: 5) {
                    Text("Binary:")
                        .fontWeight(.bold)
                    TextField("", text: $Values.binValue)
                        .textFieldStyle(RoundedBorderTextFieldStyle())
                        .frame(minWidth: 60)
                        .fixedSize(horizontal: true, vertical: /*@START_MENU_TOKEN@*/true/*@END_MENU_TOKEN@*/)
                        .disabled(true)
                }
            }
            .padding(.trailing, 40.0)
            

            Button(action: {
                self.launchBitVisualizer()
            }) {
                Text("Launch Bit Visualizer")
                    .frame(width: 200)
            }
            .padding(.all, 5)
        }
        .padding(.all, 10)
    }
    
    func numInputTextChanged(shiftedValues: Bool) {
        // Handle text changed logic
        // Update decValue, hexValue, binValue accordingly
        //TODO: need to implement try catch equivalent for error handling
        var numInput = self.numInput
        var decValue: UInt32 = 0
        var hexValue = ""
        var binValue = ""
        
        do {
            if (!shiftedValues)
            {
                numInput = numInput.trimmingCharacters(in: .whitespacesAndNewlines)
                if (numInput.isEmpty)
                {
                    decValue = 0
                    hexValue = ""
                    binValue = ""
                }
                else if (numInput.lowercased().hasPrefix("0x"))
                {
                    hexValue = String(numInput.dropFirst(2))
                    if let decValueUnwrapped = UInt32(hexValue, radix: 16)
                    {
                        decValue = decValueUnwrapped
                    }
                    else
                    {
                        
                    }
                }
            }
        }
        
    }

    func launchBitVisualizer() {
        // Handle button click logic
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
