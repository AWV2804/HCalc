//
//  Values.swift
//  HCalc
//
//  Created by Atharva Rao on 7/8/24.
//

import SwiftUI
import Combine

class Values: ObservableObject
{
    @Published var hexValue: String = ""
    @Published var binValue: String = ""
    @Published var decValue: String = ""
}
