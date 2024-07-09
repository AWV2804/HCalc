//
//  HCalcApp.swift
//  HCalc
//
//  Created by Atharva Rao on 6/19/24.
//

import SwiftUI

@main
struct HCalcApp: App {
    var values = Values()
    
    var body: some Scene {
        WindowGroup {
            ContentView()
                .environmentObject(values)
                .frame(width: 800, height: 450)
        }
    }
}
